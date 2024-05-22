using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using DDD.Core.Data.Sql.Commands.Library.Interceptors;
using DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
using DDD.EndPoints.Web.Library.Extensions.ModelBinding;
using MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Extentions;
using MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;
using Serilog;
using Zamin.Extensions.DependencyInjection;
namespace MiniBlog.EndPoints.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            IConfiguration configuration = builder.Configuration;

            //zamin
            builder.Services.AddWebApiCore("DDD", "MiniBlog");

            //microsoft
            builder.Services.AddEndpointsApiExplorer();

            //zamin
            builder.Services.AddZaminWebUserInfoService(configuration, "WebUserInfo", true);

            //zamin
            builder.Services.AddZaminParrotTranslator(configuration, "ParrotTranslator");

            //zamin
            //builder.Services.AddSoftwarePartDetector(configuration, "SoftwarePart");

            //zamin
            builder.Services.AddNonValidatingValidator();

            //zamin
            builder.Services.AddZaminMicrosoftSerializer();

            //zamin
            builder.Services.AddZaminAutoMapperProfiles(configuration, "AutoMapper");

            //zamin
            builder.Services.AddZaminInMemoryCaching();
            //builder.Services.AddZaminSqlDistributedCache(configuration, "SqlDistributedCache");

            //CommandDbContext
            builder.Services.AddDbContext<MiniBlogCommandsDbContext>(
                c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
                .AddInterceptors(new SetPersianYeKeInterceptor(),
                                 new AddAuditDataInterceptor()));

            //QueryDbContext
            builder.Services.AddDbContext<MiniBlogQueriesDbContext>(
                c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

            //PollingPublisher
            //builder.Services.AddZaminPollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
            //builder.Services.AddZaminPollingPublisher(configuration, "PollingPublisher");

            //MessageInbox
            //builder.Services.AddZaminMessageInboxDalSql(configuration, "MessageInboxSqlStore");
            //builder.Services.AddZaminMessageInbox(configuration, "MessageInbox");

            //builder.Services.AddZaminRabbitMqMessageBus(configuration, "RabbitMq");

            //builder.Services.AddZaminTraceJeager(configuration, "OpenTeletmetry");

            //builder.Services.AddIdentityServer(configuration, "OAuth");

            builder.Services.AddSwagger(configuration, "Swagger");
            return builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            //zamin
            app.UseZaminApiExceptionHandler();

            //Serilog
            app.UseSerilogRequestLogging();

            app.UseSwaggerUI("Swagger");

            app.UseStatusCodePages();

            app.UseCors(delegate (CorsPolicyBuilder builder)
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            //app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniBlog", "BlogCreated"));

            //var useIdentityServer = app.UseIdentityServer("OAuth");

            var controllerBuilder = app.MapControllers();

            //if (useIdentityServer)
            //    controllerBuilder.RequireAuthorization();

            //app.Services.GetService<SoftwarePartDetectorService>()?.Run();
            return app;
        }
    }
}
