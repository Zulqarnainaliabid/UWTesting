﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using Upwork.Testing.Common;
using Upwork.Testing.Data;
using Upwork.Testing.Data.Abstractions;
using Upwork.Testing.Data.Exceptions;
using Upwork.Testing.Data.Models.Auth;
using Upwork.Testing.Data.Services;

namespace Upwork.Testing.API
{
    /// <summary>
    /// Startup extensions.
    /// </summary>
    public static class StartupExtensions
    {
        /// <summary>
        /// Register the database connections used by the API with DI.
        /// </summary>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<UpworkTestingDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }

        public static IServiceCollection ConfigEmailSettings(this IServiceCollection services, IConfiguration Configuration)
        {

            var emailSettings = Configuration.GetSection("EmailSetting").Get<EmailConfiguration>();
            var option = Options.Create(emailSettings);
            services.AddSingleton(emailSettings);
            return services;
        }
        /// <summary>
        /// Register the database connections used by the API with DI.
        /// </summary>
        public static IServiceCollection AddDbConnections(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            /*
             * NOTE: All database queries against strings are case-insensitive unless collation is set against
             * the database, table, or column. This cannot be overridden by the client and the results are
             * evaluated locally (e.g. .Equals(string, comparison)).
             * We should avoid client evaluation where possible!
             * See - https://github.com/aspnet/EntityFrameworkCore/issues/1222#issuecomment-443119582
             */

            if (environment.IsEnvironment(Constants.Environments.Test))
            {
                return services;
            }

            return services.AddDbContext<UpworkTestingDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("Database")));
        }

        /// <summary>
        /// Configure the mapping profiles.
        /// </summary>
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            return services.AddSingleton(config.CreateMapper());

        }

        /// <summary>
        /// Register the services used by the API with DI
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddOptions()
                .AddSingleton<IEmailService, EmailService>();
        }

        /// <summary>
        /// Configure Cross-Origin Request Sharing (CORS) options.
        /// </summary>
        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                    });
            });
        }

        /// <summary>
        /// Configure project template swagger document.
        /// </summary>
        public static IServiceCollection AddSwaggerDocument(this IServiceCollection services)
        {
            return services
                .AddSwaggerDocument(config =>
                 {
                     config.PostProcess = document =>
                     {
                         document.Info.Version = "v1";
                         document.Info.Title = "Project Template API";
                         document.Info.Contact = new NSwag.OpenApiContact
                         {
                             Name = "Devices Software Experiences"
                         };
                     };
                 });
        }

        /// <summary>
        /// Startup extension method to add our custom exception handling.
        /// </summary>
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    var pathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    using var scope = context.RequestServices.CreateScope();

                    var hostEnvironment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
                    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger(pathFeature?.Error.GetType());
                    logger.LogError(pathFeature?.Error, pathFeature?.Path);

                    object exceptionJson = hostEnvironment.EnvironmentName.ToLower() switch
                    {
                        var env when
                            env == Constants.Environments.Local ||
                            env == Constants.Environments.Test ||
                            env == Constants.Environments.Dev ||
                            env == Constants.Environments.Development => new { pathFeature?.Error.Message, pathFeature?.Error.StackTrace },
                        _ => new { pathFeature?.Error.Message }
                    };

                    // if we threw the exception, ensure the response contains the right status code
                    context.Response.StatusCode = pathFeature?.Error is ExceptionBase ? (int)(pathFeature.Error as ExceptionBase)?.StatusCode : 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(exceptionJson), context.RequestAborted);
                });
            });
        }
    }
}
