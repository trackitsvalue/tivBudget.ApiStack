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
using tivBudget.Api.Options;
using Microsoft.Extensions.Options;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using tivBudget.Api.Services;
using tivBudget.Api.Services.Interfaces;

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

      // Build out BUDGETAPP and DB sections for configuration.
      services.Configure<BudgetAppOptions>(Program.Configuration.GetSection("BUDGETAPP"));
      services.Configure<DbOptions>(Program.Configuration.GetSection("DB"));

      var sp = services.BuildServiceProvider();
      var dbOptions = sp.GetService<IOptions<DbOptions>>();
      var budgetAppOptions = sp.GetService<IOptions<BudgetAppOptions>>();

      Log.Information($"DBOptions: {dbOptions.Value.ServerName}, {dbOptions.Value.UserName}");

      foreach (var env in Program.Configuration.GetChildren())
      {
        if (env.Key.CompareNoCase("DB") || (env.Key.CompareNoCase("BUDGETAPP")))
        {
          foreach (var envSub in env.GetChildren())
          {
            Log.Information($"{env.Key} - {envSub.Key}:{ envSub.Value}");
          }
        }
        else
        {
          Log.Information($"{env.Key}:{ env.Value}");
        }
      }

      Log.Information($"CORS Operations will be allowed from {budgetAppOptions.Value.ClientRootUrl}");

      services.AddCors(options =>
      {
        options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
          {
            builder.WithOrigins(budgetAppOptions.Value.ClientRootUrl) //.AllowAnyOrigin() 
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


      Log.Information($"API will be validating against '{budgetAppOptions.Value.StsAuthority}' for audience '{budgetAppOptions.Value.StsAudience}'");

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = budgetAppOptions.Value.StsAuthority;
        options.Audience = budgetAppOptions.Value.StsAudience;
      });

      // Register the Swagger generator, defining 1 or more Swagger documents
      // if (!Program.ExecutionEnvironment.IsProduction())
      // {
      services.AddSwaggerGen(c =>
       {
         c.SwaggerDoc(ApiVersion, new Info { Title = ApplicationInfo.Name, Version = ApplicationInfo.Version.ToString() });
         c.IncludeXmlComments(Path.Combine(Program.ExecutionEnvironment.ServiceRootPath, $"{ApplicationInfo.Name}.xml"));
       });
      // }

      services.AddSerilogFrameworkAgent();
      services.AddApiLoggingServices(ApplicationAssembly, "tiv-api-budget", ApiLogVerbosity.LogMinimalRequest);

      Log.Information($"DB Operations will be going against {dbOptions.Value.ServerName}");
      Log.Information($"Running in the {Program.ExecutionEnvironment.EnvironmentName} with a root path of '{Program.ExecutionEnvironment.ServiceRootPath}'");

      // TODO: Maybe different DB names per environment coming from appsettings files?
      var dbConnectionString = DbOptions.BuildConnectionString(dbOptions.Value.ServerName, dbOptions.Value.UserName, dbOptions.Value.UserPassword, "trackItsValue");

      services.AddDbContext<freebyTrackContext>(o => o.UseSqlServer(dbConnectionString));

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IBudgetRepository, BudgetRepository>();
      services.AddScoped<IBudgetCategoryTemplateRepository, BudgetCategoryTemplateRepository>();

      services.AddScoped<IAccountRepository, AccountRepository>();
      services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
      services.AddScoped<IAccountTemplateRepository, AccountTemplateRepository>();
      services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();

      services.AddScoped<IAccountService, AccountService>();
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

      // if (!Program.ExecutionEnvironment.IsProduction())
      // {
      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApplicationInfo.Name);
      });
      // }


      app.UseCors(MyAllowSpecificOrigins);
      app.UseAuthentication();

      app.UseMvc();
    }
  }
}
