using freebyTech.Common.Web.ExtensionMethods;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using freebyTech.Common.Interfaces;
using freebyTech.Common.Environment;
using freebyTech.Common.ExtensionMethods;

namespace tivBudget.Api
{
    /// <summary>
    /// Main entry point for tivBuget.Api application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The static ExecutionEnvironment defined for the application.
        /// </summary>
        public static IExecutionEnvironment ExecutionEnvironment = new ExecutionEnvironment(new EnvironmentManager());
        
        /// <summary>
        /// The static Configuraiton definde for the application.
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder().AddDefaultConfiguration(ExecutionEnvironment).Build();


        /// <summary>
        /// Main entry point for application.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .WriteTo.ApplicationInsightsWithStandardLoggersForTraceTelemetry(Configuration["ApplicationInsights.InstrumentationKey"])
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseConfiguration(Configuration)
                .UseSerilog();
    }
}
