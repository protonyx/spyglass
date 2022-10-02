using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using Spyglass.Server.Data;
using Spyglass.Server.Services;
using Spyglass.Server.MappingProfiles;
using Spyglass.Server.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Spyglass.Server
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private IWebHostEnvironment HostingEnvironment { get; }

        private Assembly StartupAssembly { get; set; }

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            this.Configuration = config;
            this.HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StartupAssembly = Assembly.GetEntryAssembly();
            
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

            // services.AddSingleton<IDataContext, SpyglassMongoContext>();
            services.AddDbContext<SpyglassDbContext>(opt =>
            {
                var dataFolder = Configuration.GetValue<string>("DataPath");
                var path = Path.GetFullPath(dataFolder);
                var dbPath = Path.Join(path, "spyglass.db");
                
                var csb = new SqliteConnectionStringBuilder()
                {
                    DataSource = dbPath
                };

                opt.UseSqlite(csb.ToString());
            });
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            services.AddScoped<IRepository<Monitor>, MonitorRepository>();
            services.AddScoped<MetricsService>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Spyglass API",
                    Version = "v1"
                });
                c.DescribeAllParametersInCamelCase();
                c.IncludeXmlComments(Path.ChangeExtension(StartupAssembly.Location, "xml"));
            });
            
            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DTOProfile>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Metrics.DefaultRegistry.AddBeforeCollectCallback(async (cancel) =>
            {
                var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                {
                    var metricsService = scope.ServiceProvider.GetRequiredService<MetricsService>();
                    await metricsService.UpdateMetricsAsync();
                }
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
