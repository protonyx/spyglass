using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using Spyglass.Server.Data;
using Spyglass.Server.Services;
using Spyglass.Server.MappingProfiles;
using Swashbuckle.AspNetCore.Swagger;

namespace Spyglass.Server
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            this.Configuration = config;
            this.HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.WriteIndented = !this.HostingEnvironment.IsProduction();
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .ConfigureApiBehaviorOptions(opt => { opt.SuppressModelStateInvalidFilter = true; });

            services.AddSingleton<IDataContext, SpyglassMongoContext>();
            services.AddSingleton<MetadataService>();

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelMetadataProfile>();
                cfg.AddProfile<DTOProfile>();
            });
            if (HostingEnvironment.IsDevelopment())
            {
                config.AssertConfigurationIsValid();
            }
            services.AddSingleton<IMapper>((sp) => config.CreateMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Spyglass API",
                    Version = "v1"
                });
                c.DescribeAllParametersInCamelCase();
                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Metrics.DefaultRegistry.AddBeforeCollectCallback(async (cancel) =>
            {
                var metricsService = app.ApplicationServices.GetService<MetricsService>();
                await metricsService.UpdateMetricsAsync();
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spyglass v1");
            });

            app.UseRouting();

//            app.UseAuthentication();
//            app.UseAuthorization();
            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });
            
            app.UseRewriter(new RewriteOptions()
                .Add(new RedirectNonFileRequestRule()));
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
