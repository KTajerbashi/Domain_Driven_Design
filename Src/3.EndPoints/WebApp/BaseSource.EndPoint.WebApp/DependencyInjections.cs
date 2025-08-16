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