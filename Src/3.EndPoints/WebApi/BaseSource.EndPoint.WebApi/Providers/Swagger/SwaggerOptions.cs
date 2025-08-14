namespace BaseSource.EndPoint.WebApi.Providers.Swagger;

public class SwaggerOptions
{
    public string Title { get; set; } = "Demo API";
    public string SecurityTitle { get; set; } = "Bearer";
    public string Version { get; set; } = "v1";
    public string Description { get; set; } = "Please enter a valid token";
    public string Name { get; set; } = "Authorization";
    public string BearerFormat { get; set; } = "JWT";
    public string Scheme { get; set; } = "Bearer";
}
