﻿using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spyglass.SDK.Converters;
using Spyglass.SDK.Services;
using Spyglass.Server.DAL;

namespace Spyglass.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new MetricProviderConverter());
                });

            services.AddSingleton<IRepositoryFactory, MongoRepositoryFactory>();
            services.AddSingleton<ProviderService>();

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(Assembly.GetEntryAssembly());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>((sp) => mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
