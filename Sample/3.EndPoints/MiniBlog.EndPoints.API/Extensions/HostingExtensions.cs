﻿using DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
using DDD.EndPoints.Web.Library.Extensions.ModelBinding;
using DDD.Infra.Data.Sql.Commands.Library.Interceptors;
using Extensions.Caching.InMemory.Extensions.DependencyInjection;
using Extensions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;
using Extensions.Serializers.Microsoft.Extensions.DependencyInjection;
using Extensions.Translations.Parrot.Extensions.DependencyInjection;
using Extensions.UsersManagement.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MiniBlog.EndPoints.API.Extensions.DependencyInjection.Swaggers.Extentions;
using MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;
using Serilog;




namespace MiniBlog.EndPoints.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// چهارمین مرحله اجرا شده نرم افزار
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            try
            {
                IConfiguration configuration = builder.Configuration;

                builder.Services.AddWebApiCore("DDD", "MiniBlog");

                //  MicroSoft
                builder.Services.AddEndpointsApiExplorer();

                // Shared Kernel
                builder.Services.AddWebUserInfoService(configuration, "WebUserInfo", true);

                // Shared Kernel
                builder.Services.AddParrotTranslator(configuration, "ParrotTranslator");

                // Shared Kernel
                //builder.Services.AddSoftwarePartDetector(configuration, "SoftwarePart");


                builder.Services.AddNonValidatingValidator();

                // Shared Kernel
                builder.Services.AddMicrosoftSerializer();

                // Shared Kernel
                builder.Services.AddAutoMapperProfiles(configuration, "AutoMapper");

                // Shared Kernel
                builder.Services.AddKernelInMemoryCaching();
                //builder.Services.AddKernelSqlDistributedCache(configuration, "SqlDistributedCache");
                //builder.Services.AddKernelRedisDistributedCache(configuration, "RedisDistributedCache");



                //CommandDbContext
                builder.Services.AddDbContext<MiniBlogCommandsDbContext>(
                    c => c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
                    .AddInterceptors(new SetPersianYeKeInterceptor(),
                                     new AddAuditDataInterceptor()));

                //QueryDbContext
                builder.Services.AddDbContext<MiniBlogQueriesDbContext>(
                    c => c.UseSqlServer(configuration.GetConnectionString("QueryDb_ConnectionString")));

                //PollingPublisher
                //builder.Services.AddPollingPublisherDalSql(configuration, "PollingPublisherSqlStore");
                //builder.Services.AddPollingPublisher(configuration, "PollingPublisher");

                //MessageInbox
                //builder.Services.AddMessageInboxDalSql(configuration, "MessageInboxSqlStore");
                //builder.Services.AddMessageInbox(configuration, "MessageInbox");

                //builder.Services.AddRabbitMqMessageBus(configuration, "RabbitMq");

                //builder.Services.AddTraceJeager(configuration, "OpenTeletmetry");

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
        /// =====================
        /// مرحله پنجم اجرای نرم افزار
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
