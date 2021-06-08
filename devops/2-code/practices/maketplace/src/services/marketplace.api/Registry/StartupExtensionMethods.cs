using System;
using System.Collections.Generic;
using System.Net.Http;
using EventStore.ClientAPI;
using marketplace.api.Applications;
using marketplace.api.Applications.Command;
using marketplace.api.Applications.Contracts;
using marketplace.api.Applications.Queries;
using marketplace.api.Applications.Services;
using marketplace.api.External;
using marketplace.api.Infrastructure;
using marketplace.api.ViewModels;
using marketplace.domain.kernel;
using marketplace.domain.kernel.commands;
using marketplace.domain.repositories;
using marketplace.infrastructure;
using marketplace.infrastructure.repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace marketplace.api.Registry
{
    public static class StartupExtensionMethods
    {
        public const string CorsPolicy = "ClassifiedAdPolicy";
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddDbContext<ClassifiedAdContext>(
                    options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                        options.EnableSensitiveDataLogging();
                    });
            return services;
        }

        public static IServiceCollection AddEventStore(this IServiceCollection services,IWebHostEnvironment env,IConfiguration configuration)
        {
            var eventStoreConnectStr = configuration["eventStore:connectionString"];
            var connection = EventStoreConnection.Create(
                eventStoreConnectStr,
                ConnectionSettings.Create().KeepReconnecting(),
                env.ApplicationName);
            services.AddSingleton(connection);
            services.AddSingleton<IAggregateStore>(x=>new AggregateStore(connection));
            var items = new List<ClassifiedAdDetailsViewModel>();
            services.AddSingleton<IEnumerable<ClassifiedAdDetailsViewModel>>(items);
            var subscription = new EsSubscription(connection, items);
            services.AddSingleton<IHostedService>(new HostedService(connection,subscription));
            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(control =>
            {
                control.AllowEmptyInputInBodyModelBinding = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            return services;
        }

        public static IServiceCollection SetCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options=>
            {
                options.AddPolicy(CorsPolicy, 
                                builder=>
                                {
                                    builder.WithOrigins("https://localhost:5002")
                                    .SetIsOriginAllowed((host)=>true)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
                
            });
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IClassifiedAdQueries, ClassifiedAdQueries>();
            // services.AddScoped<IAppService,ClassifiedAdAppService>();
            services.AddScoped<IAppService, ClassifiedAdEsAppService>();
            services.AddScoped<ICommandHandler<ClassifiedAds.V1.Create>,ClassifiedAdCreatedCommand>();
            services.AddSingleton<PurgomalumClient>();
            services.AddHttpClient<PurgomalumClient>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(2))
            .ConfigurePrimaryHttpMessageHandler((c) =>
                new HttpClientHandler()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; },
                });

            services.AddScoped<ICommandHandler<RegisterUserCommand>,RegisterUserCommandHandler>(command=>
                new RegisterUserCommandHandler(
                    command.GetService<IUserRepository>(),
                      text =>  command.GetService<PurgomalumClient>().CheckForProfanity(text).GetAwaiter().GetResult()
                ));

            services.AddScoped<ICommandHandler<UpdateUserFullNameCommand>, UpdateUserFullNameCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateUserDisplayNameCommand>, UpdateUserDisplayNameCommandHandler>(
                command=>
                new UpdateUserDisplayNameCommandHandler(
                    command.GetService<IUserRepository>(),
                      text =>  command.GetService<PurgomalumClient>().CheckForProfanity(text).GetAwaiter().GetResult()
                )
            );
            services.AddScoped<ICommandHandler<UpdateUserProfilePhotoCommand>, UpdateUserProfilePhotoCommandHandler>();
            return services;
        }

        public static IServiceCollection SwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClassifiedAd", Version = "v1" });
            });
            return services;
        }
    }
}