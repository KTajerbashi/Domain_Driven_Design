using DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
using DDD.EndPoints.Web.Library.Extensions.ModelBinding;
using DDD.Infra.Data.Sql.Commands.Library.Interceptors;
using Extensions.Translations.Parrot.Extensions.DependencyInjection;
using Extensions.UsersManagement.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Extentions;
using MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;
using Serilog;
using Extensions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;
using Extensions.Serializers.Microsoft.Extensions.DependencyInjection;
using Extensions.Caching.InMemory.Extensions.DependencyInjection;
using Extensions.Caching.Distributed.Sql.Extensions.DependencyInjection;
using Extensions.Caching.Distributed.Redis.Extensions.DependencyInjection;




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
            try
            {
                IConfiguration configuration = builder.Configuration;

                //zamin
                builder.Services.AddWebApiCore("DDD", "MiniBlog");

                //microsoft
                builder.Services.AddEndpointsApiExplorer();

                //zamin
                builder.Services.AddWebUserInfoService(configuration, "WebUserInfo", true);

                //zamin
                builder.Services.AddParrotTranslator(configuration, "ParrotTranslator");

                //zamin
                //builder.Services.AddSoftwarePartDetector(configuration, "SoftwarePart");

                //zamin
                builder.Services.AddNonValidatingValidator();

                //zamin
                builder.Services.AddMicrosoftSerializer();

                //zamin
                builder.Services.AddAutoMapperProfiles(configuration, "AutoMapper");

                //Kernel
                builder.Services.AddKernelInMemoryCaching();
                //builder.Services.AddKernelSqlDistributedCache(configuration, "SqlDistributedCache");
                //builder.Services.AddKernelRedisDistributedCache(configuration,"RedisDistributedCache");



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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseApiExceptionHandler();

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
