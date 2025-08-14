using Microsoft.OpenApi.Models;

namespace BaseSource.EndPoint.WebApi.Providers.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerProvider(this IServiceCollection services, IConfiguration configuration)
    {
        SwaggerOptions options = new SwaggerOptions();
        configuration.Bind("Swagger", options);
        // Configure Swagger
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc(options.Version, new OpenApiInfo { Title = options.Title, Version = options.Version });
            option.AddSecurityDefinition(options.SecurityTitle, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = options.Description,
                Name = options.Name,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = options.BearerFormat,
                Scheme = options.Scheme
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = options.Scheme
                        }
                    },
                    new string[]{}
                }
            });
        });

        return services;
    }

    public static void UseSwaggerProvider(this WebApplication app)
    {
        app.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = "swagger";
        });
    }
}
