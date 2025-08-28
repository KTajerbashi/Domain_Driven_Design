using BaseSource.EndPoint.WebApi.Middlewares.Exceptions;
using BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.EndPoint.WebApp;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddWebAppServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddHttpClient("API", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
            // Add any default headers if needed
        });

        // Configure JWT and JWE settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.Configure<JweSettings>(builder.Configuration.GetSection("JweSettings"));
        builder.Services.Configure<IdentityOptionsSettings>(builder.Configuration.GetSection("IdentityOptions"));
        
        // Configure Identity options from settings
        builder.Services.Configure<IdentityOptions>(options =>
        {
            var identitySettings = builder.Configuration.GetSection("IdentityOptions").Get<IdentityOptionsSettings>();

            options.Password.RequiredLength = identitySettings?.Password.RequiredLength ?? 6;
            options.Password.RequiredUniqueChars = identitySettings?.Password.RequiredUniqueChars ?? 1;
            options.Password.RequireNonAlphanumeric = identitySettings?.Password.RequireNonAlphanumeric ?? true;
            options.Password.RequireLowercase = identitySettings?.Password.RequireLowercase ?? true;
            options.Password.RequireUppercase = identitySettings?.Password.RequireUppercase ?? true;
            options.Password.RequireDigit = identitySettings?.Password.RequireDigit ?? true;

            options.Lockout.DefaultLockoutTimeSpan = identitySettings?.Lockout.DefaultLockoutTimeSpan ?? TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = identitySettings?.Lockout.MaxFailedAccessAttempts ?? 5;
            options.Lockout.AllowedForNewUsers = identitySettings?.Lockout.AllowedForNewUsers ?? true;

            options.User.RequireUniqueEmail = identitySettings?.User.RequireUniqueEmail ?? true;
            options.User.AllowedUserNameCharacters = identitySettings?.User.AllowedUserNameCharacters ?? "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            options.SignIn.RequireConfirmedEmail = identitySettings?.SignIn.RequireConfirmedEmail ?? false;
            options.SignIn.RequireConfirmedPhoneNumber = identitySettings?.SignIn.RequireConfirmedPhoneNumber ?? false;
        });

        builder.Services.AddControllers();

        builder.Services.AddRazorPages();

        return builder;
    }
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<InitialDatabaseContext>();
        await initialiser.RunAsync();
    }
    public static WebApplication UseWebAppServices(this WebApplication app)
    {
        // ========================
        // 1. Environment-specific middleware
        // ========================
        if (!app.Environment.IsDevelopment())
        {
            // Production error page
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        else
        {
            // Swagger in development
            app.UseSwaggerProvider();
        }

        // ========================
        // 2. Security & HTTPS
        // ========================
        app.UseHttpsRedirection();

        // ========================
        // 3. Static files & assets
        // ========================
        app.MapStaticAssets();

        // ========================
        // 4. Routing
        // ========================
        app.UseRouting();

        // ========================
        // 5. Global API exception handler
        // ========================
        app.UseApiExceptionHandler(); // logs exceptions with all context properties

        // ========================
        // 6. Authentication & Authorization
        // ========================
        app.UseAuthentication();
        app.UseAuthorization();

        // ========================
        // 7. Serilog enrichment for requests
        // ========================
        app.UseSerilog(); // logs request info, duration, user, controller/action, etc.

        // ========================
        // 8. Endpoint mapping
        // ========================
        app.MapControllers();

        app.MapRazorPages()
           .WithStaticAssets(); // custom convention for /App prefix

        // Redirect root to /App
        app.MapGet("/", () => Results.Redirect("/App"));

        return app;
    }
}