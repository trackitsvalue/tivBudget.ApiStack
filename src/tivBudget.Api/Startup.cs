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
using Newtonsoft.Json;
using tivBudget.Dal.Repositories;
using tivBudget.Dal.Repositories.Interfaces;
using tivBudget.Dal.Models;
using Microsoft.ApplicationInsights.Extensibility;
using freebyTech.Common.Web.Logging.Initializers.AppInsights;

namespace tivBudget.Api
{
  /// <summary>
  /// Startup class for tivBudget.Api application.
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Startup class constructor
    /// </summary>
    public Startup()
    {
      ApplicationAssembly = Assembly.GetExecutingAssembly();
      ApplicationInfo = ApplicationAssembly.GetName();
      ApiVersion = $"v{ApplicationInfo.Version.Major}.{ApplicationInfo.Version.Minor}";
    }

    /// <summary>
    /// The Application Assembly
    /// </summary>
    public Assembly ApplicationAssembly { get; }

    /// <summary>
    /// The AssemblyName of the Application
    /// </summary>
    public AssemblyName ApplicationInfo { get; private set; }

    /// <summary>
    /// The ApiVersion as defined for Swagger display
    /// </summary>
    public string ApiVersion { get; private set; }

    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      TelemetryConfiguration.Active.TelemetryInitializers.Add(new ContextInitializer(ApplicationInfo.Name));

      services.AddCors(options =>
      {
        options.AddPolicy(MyAllowSpecificOrigins,
          builder =>
          {
            builder.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });


      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
          .AddJsonOptions(options =>
          {
            // Stop parent child reference issues with entities.
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
          });
      //.AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
      //.AddMvcOptions(i => i.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(new MvcOptions())));


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
      services.AddScoped<IBudgetCategoryTemplateRepository, BudgetCategoryTemplateRepository>();

      services.AddScoped<IAccountRepository, AccountRepository>();
      services.AddScoped<IAccountCategoryTemplateRepository, AccountCategoryTemplateRepository>();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
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

      app.UseCors(MyAllowSpecificOrigins);

      app.UseMvc();
    }
  }
}
