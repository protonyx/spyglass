using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spyglass.SDK.Data;
using Spyglass.Server.Converters;
using Spyglass.Server.Services;
using Spyglass.Data.MongoDb;
using Spyglass.Server.MappingProfiles;
using Swashbuckle.AspNetCore.Swagger;

namespace Spyglass.Server
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            this.Configuration = config;
            this.HostingEnvironment = env;
        }

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
                cfg.AddProfile<ModelMetadataProfile>();
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
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spyglass v1");
            });

            app.UseRewriter(new RewriteOptions()
                .Add(new RedirectNonFileRequestRule()));
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
