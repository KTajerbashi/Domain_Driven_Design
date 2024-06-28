using Microsoft.OpenApi.Models;
using MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Extentions;
using MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Filters;
using MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Extentions;

public static class SwaggerExtensions
{
    /// <summary>
    /// در ادامه مرحله چهارم
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        var swaggerOption = configuration.GetSection(sectionName).Get<SwaggerOption>();

        if (swaggerOption != null && swaggerOption.SwaggerDoc != null && swaggerOption.Enabled == true)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(swaggerOption.SwaggerDoc.Name, new OpenApiInfo
                {
                    Title = swaggerOption.SwaggerDoc.Title,
                    Version = swaggerOption.SwaggerDoc.Version
                });

                var oAuthOption = configuration.GetSection("OAuth").Get<SwaggerOAuthOption>();
                if (oAuthOption != null && oAuthOption.Enabled)
                {
                    o.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "OAuth2",
                        BearerFormat = "Bearer <token>",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(oAuthOption.AuthorizationUrl),
                                TokenUrl = new Uri(oAuthOption.TokenUrl),
                                Scopes = oAuthOption.Scopes
                            }
                        },
                    }); ;

                    o.OperationFilter<AddParamsToHeader>();
                }
            });
        }

        return services;
    }

    /// <summary>
    /// در ادامه مرحله پنجم و گام دوم
    /// </summary>
    /// <param name="app"></param>
    /// <param name="sectionName"></param>
    public static void UseSwaggerUI(this WebApplication app, string sectionName)
    {
        var swaggerOption = app.Configuration.GetSection(sectionName).Get<SwaggerOption>();

        if (swaggerOption != null && swaggerOption.SwaggerDoc != null && swaggerOption.Enabled == true)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.DocExpansion(DocExpansion.None);
                option.SwaggerEndpoint(swaggerOption.SwaggerDoc.URL, swaggerOption.SwaggerDoc.Title);
                option.RoutePrefix = string.Empty;
                option.OAuthUsePkce();
            });
        }
    }
}
