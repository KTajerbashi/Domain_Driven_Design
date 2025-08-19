using BaseSource.EndPoint.WebApi.Middlewares.Exceptions;

namespace BaseSource.EndPoint.WebApp;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddWebAppServices(this WebApplicationBuilder builder)
    {
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