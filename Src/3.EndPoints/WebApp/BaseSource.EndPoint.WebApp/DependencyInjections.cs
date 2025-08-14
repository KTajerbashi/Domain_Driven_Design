using BaseSource.EndPoint.WebApi;
using BaseSource.EndPoint.WebApi.Providers.Swagger;
using BaseSource.Infrastructure.SQL.Command.Persistence;
using System.Reflection;

namespace BaseSource.EndPoint.WebApp;

public static class DependencyInjections
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
    {
        services.AddControllers();

        services.AddRazorPages();

        services.AddWebApiServices(configuration);

        return services;
    }
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<InitialDatabaseContext>();
        await initialiser.RunAsync();
    }
    public static WebApplication UseWebAppServices(this WebApplication app)
    {

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        else
        {
            app.UseSwaggerProvider();
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