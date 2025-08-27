using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;

public class IdentityFactory : IIdentityFactory
{
    private readonly ILogger<IdentityFactory> _logger;
    private readonly CommandDatabaseContext _context;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly RoleManager<RoleIdentity> _roleManager;
    private readonly SignInManager<UserIdentity> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly JweSettings _jweSettings;
    private readonly IdentityOptionsSettings _identityOptions;
    private readonly ActivitySource _activitySource;

    public IdentityFactory(
        ILogger<IdentityFactory> logger,
        CommandDatabaseContext context,
        UserManager<UserIdentity> userManager,
        RoleManager<RoleIdentity> roleManager,
        SignInManager<UserIdentity> signInManager,
        IOptions<JwtSettings> jwtOptions,
        IOptions<JweSettings> jweOptions,
        IOptions<IdentityOptionsSettings> identityOptions)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtSettings = jwtOptions.Value;
        _jweSettings = jweOptions.Value;
        _identityOptions = identityOptions.Value;
        _activitySource = new ActivitySource("IdentityFactory");
        ValidateConfiguration();
    }
    private void ValidateConfiguration()
    {
        if (string.IsNullOrEmpty(_jwtSettings.Secret) || _jwtSettings.Secret.Length < 32)
        {
            throw new ArgumentException("JWT Secret must be at least 32 characters long");
        }

        if (string.IsNullOrEmpty(_jweSettings.EncryptionKey) || _jweSettings.EncryptionKey.Length < 32)
        {
            throw new ArgumentException("JWE Encryption Key must be at least 32 characters long");
        }

        if (_jwtSettings.TokenExpirationMinutes <= 0)
        {
            throw new ArgumentException("Token expiration must be greater than 0");
        }
    }
    public async Task<AuthenticationResult> LoginAsync(string username, string password, bool rememberMe = false)
    {
        using var activity = _activitySource.StartActivity("Login", ActivityKind.Server);
        activity?.SetTag("user.username", username);

        try
        {
            _logger.LogInformation("Starting login process for user {Username}", username);

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                _logger.LogWarning("Login failed: User {Username} not found", username);
                activity?.SetStatus(ActivityStatusCode.Error, "User not found");
                return AuthenticationResult.Failed("Invalid credentials");
            }

            // Check password
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            if (!signInResult.Succeeded)
            {
                _logger.LogWarning("Login failed: Invalid password for user {Username}", username);
                activity?.SetStatus(ActivityStatusCode.Error, "Invalid password");
                return AuthenticationResult.Failed("Invalid credentials");
            }

            if (!user.IsActive)
            {
                _logger.LogWarning("Login failed: User {Username} is inactive", username);
                activity?.SetStatus(ActivityStatusCode.Error, "User inactive");
                return AuthenticationResult.Failed("Account is inactive");
            }

            // Clear any existing session data before creating new one
            await CleanupUserSession(user.Id);

            // Generate tokens
            var token = await GenerateJwtTokenAsync(user);
            var refreshToken = await GenerateRefreshTokenAsync(user);

            // Add user claims
            await AddUserClaimsAsync(user);

            // Add user login record
            await AddUserLoginRecordAsync(user, "JWT", $"token_{user.Id}");

            // Add user token
            await AddUserTokenRecordAsync(user, refreshToken);

            // Update last login timestamp
            //user.LastLoginDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            activity?.SetTag("user.id", user.Id);
            activity?.SetStatus(ActivityStatusCode.Ok);
            _logger.LogInformation("User {Username} logged in successfully", username);
            await _signInManager.SignInAsync(user, rememberMe);
            return AuthenticationResult.Success(token, refreshToken, DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationMinutes));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for user {Username}", username);
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            return AuthenticationResult.Failed("An error occurred during login");
        }
    }

    public async Task SignOutAsync(long userId)
    {
        using var activity = _activitySource.StartActivity("SignOut", ActivityKind.Server);
        activity?.SetTag("user.id", userId);

        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("Sign out failed: User {UserId} not found", userId);
                return;
            }

            // Remove all session-related data
            await CleanupUserSession(userId);

            // Sign out from SignInManager (if using cookie authentication)
            await _signInManager.SignOutAsync();

            // Update last logout timestamp
            //user.LastLogoutDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            activity?.SetStatus(ActivityStatusCode.Ok);
            _logger.LogInformation("User {UserId} signed out successfully", userId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during sign out for user {UserId}", userId);
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            throw;
        }
    }


    private async Task CleanupUserSession(long userId)
    {
        // Remove all user claims
        await RemoveAllUserClaimsAsync(userId);

        // Remove all user tokens
        await RemoveAllUserTokensAsync(userId);

        // Remove all user logins (optional - you might want to keep login history)
        // await RemoveAllUserLoginsAsync(userId);

        // Remove role claims from user (but keep role assignments)
        await CleanupRoleClaimsAsync(userId);
    }

    private async Task AddUserClaimsAsync(UserIdentity user)
    {
        var claims = new List<Claim>
        {
            new Claim("LastLogin", DateTime.Now.ToString("O")),
            new Claim("SessionId", Guid.NewGuid().ToString()),
            new Claim("AuthMethod", "JWT"),
            new Claim("UserId", $"{user.Id}"),
            new Claim("UserName", user.UserName),
            new Claim("Name", user.FirstName),
            new Claim("IsActive", user.IsActive.ToString())
        };

        // Add user-specific claims
        if (!string.IsNullOrEmpty(user.Email))
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

        // Add current roles as claims
        var userRoles = await _userManager.GetRolesAsync(user);
        var roleEntity = await _roleManager.FindByNameAsync(userRoles.First());
        claims.Add(new Claim("UserRoleId", $"{roleEntity.Id}"));
        foreach (var role in userRoles)
        {
            claims.Add(new Claim("CurrentRole", role));
        }

        await _userManager.AddClaimsAsync(user, claims);
    }

    private async Task RemoveAllUserClaimsAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            // Remove only session-specific claims, keep permanent claims
            //var sessionClaims = claims.Where(c =>
            //    c.Type == "LastLogin" ||
            //    c.Type == "SessionId" ||
            //    c.Type == "AuthMethod" ||
            //    c.Type == "CurrentRole" ||
            //    c.Type == "IsActive");

            await _userManager.RemoveClaimsAsync(user, claims);
        }
    }

    private async Task AddUserLoginRecordAsync(UserIdentity user, string loginProvider, string providerKey)
    {
        var loginInfo = new UserLoginInfo(loginProvider, providerKey, loginProvider);

        // Check if login already exists
        var existingLogins = await _userManager.GetLoginsAsync(user);
        if (!existingLogins.Any(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey))
        {
            await _userManager.AddLoginAsync(user, loginInfo);
        }
    }

    private async Task AddUserTokenRecordAsync(UserIdentity user, string refreshToken)
    {
        // Store refresh token
        await _userManager.SetAuthenticationTokenAsync(user, "JWT", "RefreshToken", refreshToken);

        // Store access token timestamp
        await _userManager.SetAuthenticationTokenAsync(user, "JWT", "TokenIssuedAt", DateTime.UtcNow.ToString("O"));
    }

    private async Task RemoveAllUserTokensAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            // Remove refresh token
            await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "RefreshToken");

            // Remove token timestamp
            await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "TokenIssuedAt");

            // Remove any other session tokens
            var allTokens = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "AllTokens");
            if (!string.IsNullOrEmpty(allTokens))
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "AllTokens");
            }
        }
    }

    private async Task CleanupRoleClaimsAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roleClaims = claims.Where(c => c.Type == "CurrentRole").ToList();

            if (roleClaims.Any())
            {
                await _userManager.RemoveClaimsAsync(user, roleClaims);
            }
        }
    }

    public async Task<string> GenerateJwtTokenAsync(UserIdentity user)
    {
        using var activity = _activitySource.StartActivity("GenerateJwtToken", ActivityKind.Internal);
        activity?.SetTag("user.id", user.Id);

        var claims = await GetUserClaimsAsync(user);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateJweTokenAsync(UserIdentity user)
    {
        var claims = await GetUserClaimsAsync(user);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jweSettings.EncryptionKey));
        var encryptingCredentials = new EncryptingCredentials(
            key,
            _jweSettings.KeyManagementAlgorithm,
            _jweSettings.EncryptionAlgorithm);

        var signCreden = new SigningCredentials(key, _jweSettings.EncryptionAlgorithm);
        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims,
            null,
            DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationMinutes),
            signCreden
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(UserIdentity user)
    {
        var refreshToken = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        refreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(refreshToken));

        // Store refresh token
        await AddUserTokenAsync(user.Id, "RefreshToken", "Refresh", refreshToken);

        return refreshToken;
    }

    public async Task<string> GenerateJweRefreshTokenAsync(UserIdentity user)
    {
        var refreshToken = await GenerateRefreshTokenAsync(user);

        // Encrypt the refresh token
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jweSettings.EncryptionKey));
        var encryptingCredentials = new EncryptingCredentials(
            key,
            _jweSettings.KeyManagementAlgorithm,
            _jweSettings.EncryptionAlgorithm);

        var token = new JwtSecurityToken(claims: new[] { new Claim("refresh_token", refreshToken) });

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<TokenValidationResult> ValidateTokenAsync(string token)
    {
        using var activity = _activitySource.StartActivity("ValidateToken", ActivityKind.Internal);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            activity?.SetStatus(ActivityStatusCode.Ok);
            return TokenValidationResult.Success(principal);
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogWarning(ex, "Token validation failed");
            activity?.SetStatus(ActivityStatusCode.Error, "Security token exception");
            return TokenValidationResult.Failed("Invalid token");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token validation error");
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            return TokenValidationResult.Failed("Token validation error");
        }
    }

    public async Task<bool> RevokeTokenAsync(string token, long userId)
    {
        using var activity = _activitySource.StartActivity("RevokeToken", ActivityKind.Server);
        activity?.SetTag("user.id", userId);

        try
        {
            // Add token to blacklist
            await _context.RevokedTokens.AddAsync(new RevokedToken
            {
                Token = token,
                UserId = userId,
                RevokedAt = DateTime.UtcNow,
                //Reason = "Manual revocation"
            });

            // Also remove the refresh token
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "RefreshToken");
                await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "TokenIssuedAt");
            }

            await _context.SaveChangesAsync();

            activity?.SetStatus(ActivityStatusCode.Ok);
            _logger.LogInformation("Token revoked successfully for user {UserId}", userId);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to revoke token for user {UserId}", userId);
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            return false;
        }
    }



    public async Task<IdentityResult> AddClaimsAsync(long userId, IEnumerable<Claim> claims)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        return await _userManager.AddClaimsAsync(user, claims);
    }

    public async Task<IdentityResult> RemoveClaimsAsync(long userId, IEnumerable<string> claimTypes)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var claims = await _userManager.GetClaimsAsync(user);
        var claimsToRemove = claims.Where(c => claimTypes.Contains(c.Type));

        return await _userManager.RemoveClaimsAsync(user, claimsToRemove);
    }

    public async Task<IdentityResult> AddUserTokenAsync(long userId, string loginProvider, string name, string value)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var token = new UserTokenIdentity()
        {
            UserId = userId,
            LoginProvider = loginProvider,
            Name = name,
            Value = value
        };

        await _userManager.SetAuthenticationTokenAsync(user, loginProvider, name, value);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> RemoveUserTokenAsync(long userId, string loginProvider, string name)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        await _userManager.RemoveAuthenticationTokenAsync(user, loginProvider, name);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> AddUserLoginAsync(long userId, string loginProvider, string providerKey)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var login = new UserLoginInfo(loginProvider, providerKey, loginProvider);
        return await _userManager.AddLoginAsync(user, login);
    }

    public async Task<IdentityResult> RemoveUserLoginAsync(long userId, string loginProvider, string providerKey)
    {
        var user = await _userManager.FindByIdAsync($"{userId}");
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        return await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
    }


    public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
    {
        using var activity = _activitySource.StartActivity("RefreshToken", ActivityKind.Server);

        try
        {
            var validationResult = await ValidateTokenAsync(token);
            if (!validationResult.IsValid)
                return AuthenticationResult.Failed("Invalid token");

            var userId = validationResult.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
                return AuthenticationResult.Failed("Invalid token claims");

            // Validate refresh token
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return AuthenticationResult.Failed("User not found");

            var isValidRefreshToken = await ValidateRefreshTokenAsync(user, refreshToken);
            if (!isValidRefreshToken)
                return AuthenticationResult.Failed("Invalid refresh token");

            // Clean up old session data
            await CleanupUserSession(userIdLong);

            // Generate new tokens
            var newToken = await GenerateJwtTokenAsync(user);
            var newRefreshToken = await GenerateRefreshTokenAsync(user);

            // Add new session claims
            await AddUserClaimsAsync(user);

            // Update token records
            await AddUserTokenRecordAsync(user, newRefreshToken);

            activity?.SetStatus(ActivityStatusCode.Ok);
            _logger.LogInformation("Token refreshed successfully for user {UserId}", userId);

            return AuthenticationResult.Success(newToken, newRefreshToken,
                DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationMinutes));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token refresh failed");
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            return AuthenticationResult.Failed("An error occurred during token refresh");
        }
    }

    private async Task<bool> ValidateRefreshTokenAsync(UserIdentity user, string refreshToken)
    {
        var storedToken = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "RefreshToken");

        if (string.IsNullOrEmpty(storedToken) || storedToken != refreshToken)
            return false;

        // Check if refresh token is expired
        var tokenIssuedAt = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "TokenIssuedAt");
        if (DateTime.TryParse(tokenIssuedAt, out var issuedAt))
        {
            var refreshTokenExpiration = issuedAt.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            if (DateTime.UtcNow > refreshTokenExpiration)
                return false;
        }

        return true;
    }

    public async Task<bool> IsUserActiveAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user?.IsActive == true;
    }

    public async Task<bool> HasValidSessionAsync(long userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;

        var refreshToken = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "RefreshToken");
        return !string.IsNullOrEmpty(refreshToken);
    }
    private async Task<List<Claim>> GetUserClaimsAsync(UserIdentity user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("UserName", user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim("AuthTime", DateTime.UtcNow.ToString("O"))
    };

        // Add email if available
        if (!string.IsNullOrEmpty(user.Email))
        {
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        }

        // Add phone number if available
        if (!string.IsNullOrEmpty(user.PhoneNumber))
        {
            claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
        }

        // Add user-specific claims from database
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims.Where(c => !claims.Any(existing => existing.Type == c.Type)));

        // Add role claims
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));

            // Add role-specific claims
            var roleEntity = await _roleManager.FindByNameAsync(role);
            if (roleEntity != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(roleEntity);
                claims.AddRange(roleClaims.Where(rc => !claims.Any(c => c.Type == rc.Type && c.Value == rc.Value)));
            }
        }

        // Add security stamp for token validation
        claims.Add(new Claim("SecurityStamp", user.SecurityStamp));

        return claims;
    }

}



