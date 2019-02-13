using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Web.ExtensionMethods;
using freebyTech.Common.Web.Logging.LoggerTypes;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.EntityFrameworkCore;
using tivBudget.Dal.Repositories;
using tivBudget.Dal.Repositories.Interfaces;
using tivBudget.Dal.Models;

namespace tivBudget.Api
{
    public class Startup
    {
        public Startup()
        {
            ApplicationAssembly = Assembly.GetExecutingAssembly();
            ApplicationInfo = ApplicationAssembly.GetName();
            ApiVersion = $"v{ApplicationInfo.Version.Major}.{ApplicationInfo.Version.Minor}";
        }

        public Assembly ApplicationAssembly { get; }

        public AssemblyName ApplicationInfo { get; private set; }

        public string ApiVersion { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TelemetryConfiguration.Active.TelemetryInitializers.Add(new ContextInitializer(ApplicationInfo.Name));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                .AddMvcOptions(i => i.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(new MvcOptions())));

            
            //services.AddMvcCore().AddAuthorization().AddJsonFormatters();

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.Audience = "api1";
            //    });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new Info { Title = ApplicationInfo.Name, Version = ApplicationInfo.Version.ToString() });
                c.IncludeXmlComments(Path.Combine(Program.ExecutionEnvironment.ServiceRootPath, $"{ApplicationInfo.Name}.xml"));
            });

            services.AddSerilogFrameworkAgent();
            services.AddApiLoggingServices(ApplicationAssembly, "tiv-api-budget", ApiLogVerbosity.LogMinimalRequest);

            var connectionString = Program.Configuration["ConnectionStrings:freebyTrack"];
            services.AddDbContext<freebyTrackContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IBudgetRepository, BudgetRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseStandardApiMiddleware();

            //app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApplicationInfo.Name);
            });

            app.UseMvc();
        }
    }
}
