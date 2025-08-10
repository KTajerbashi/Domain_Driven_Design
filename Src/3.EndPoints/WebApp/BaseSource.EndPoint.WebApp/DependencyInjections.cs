namespace BaseSource.EndPoint.WebApp;

public static class DependencyInjections
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllers();
        services.AddRazorPages();

        return services;
    }

    public static WebApplication UseWebAppServices(this WebApplication app)
    {

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllers();

        // Map Razor Pages (with /App prefix thanks to convention)
        app.MapRazorPages()
           .WithStaticAssets();

        app.MapGet("/", () => Results.Redirect("/App"));

        return app;
    }
}