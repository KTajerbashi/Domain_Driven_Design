namespace BaseSource.Infrastructure.SQL.Command.Persistence;

public class InitialDatabaseContext : IScopedLifetime
{
    private readonly ILogger<InitialDatabaseContext> _logger;
    private readonly CommandDatabaseContext _context;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly RoleManager<RoleIdentity> _roleManager;

    public InitialDatabaseContext(
        ILogger<InitialDatabaseContext> logger,
        CommandDatabaseContext context,
        UserManager<UserIdentity> userManager,
        RoleManager<RoleIdentity> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task RunAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();

        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task SeedRolesAsync()
    {
        await CreateRoleIfNotExistsAsync(Roles.Administrator);
        await CreateRoleIfNotExistsAsync(Roles.User);
    }
    private async Task CreateRoleIfNotExistsAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var role = new RoleIdentity(roleName, roleName);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                _logger.LogError($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }

    private async Task SeedUsersAsync()
    {
        //UserCreateParameters Parameters,string password, string Role

        List<(UserCreateParameters user, string password, string role)> usersToSeed = new List<(UserCreateParameters user, string password, string role)>()
        {
            new() {
                user = new UserCreateParameters("tajerbashi","tajerbashi@mail.com","Kamran","Tajerbashi","10010001000","+145687"),
                password = "Admin123!",
                role = Roles.Administrator
            },
            new() {
                user = new UserCreateParameters("trump","donald-trump@mail.com","Donald","Trump","20020003000","+144568"),
                password = "User123!",
                role = Roles.User
            },
        };
        foreach (var (user, password, role) in usersToSeed)
        {
            await CreateUserAsync(user, password, role);
        }
    }
    private async Task CreateUserAsync(UserCreateParameters parameters, string password, string roleName)
    {
        var user = new UserIdentity(parameters);
        bool userExists = await _userManager.Users
            .AnyAsync(u => u.UserName == user.UserName || u.Email == user.Email);

        if (userExists)
        {
            _logger.LogWarning($"User '{user.UserName}' or email '{user.Email}' already exists.");
            return;
        }

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            _logger.LogError($"Failed to create user '{user.UserName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            return;
        }

        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            _logger.LogWarning($"Role '{roleName}' not found. User '{user.UserName}' was created without role assignment.");
            return;
        }

        _context.UserRoles.Add(new UserRoleIdentity(user.Id, role.Id, true));
        await _context.SaveChangesAsync();

        _logger.LogInformation($"User '{user.UserName}' assigned to '{roleName}' role.");
    }
}
