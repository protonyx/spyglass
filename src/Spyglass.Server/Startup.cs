using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spyglass.SDK.Data;
using Spyglass.Server.Converters;
using Spyglass.Server.Services;
using Spyglass.Data.MongoDb;
using Swashbuckle.AspNetCore.Swagger;

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

            if (env.IsDevelopment())
            {
              builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this.Configuration);

            services.AddCors();
          
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new MetricProviderConverter());
                });

            services.AddSingleton<IDataContext, SpyglassMongoContext>();
            services.AddSingleton<MetadataService>();

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(Assembly.GetEntryAssembly());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>((sp) => mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Title = "Spyglass API",
                    Version = "v1"
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Spyglass.Server.xml");
                c.IncludeXmlComments(filePath);

                c.MapType<Guid>(() => new Schema() { Type = "string (Guid)"});
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Setup logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spyglass v1");
            });

            app.UseMvc();
        }
    }
}
