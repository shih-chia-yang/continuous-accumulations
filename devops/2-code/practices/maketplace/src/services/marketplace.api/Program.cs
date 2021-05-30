using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace marketplace.api
{
    public class Program
    {
        private static string AppPath => Directory.GetCurrentDirectory();
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration);
            // var Configuration = GetConfiguration();
            var host = CreateHostBuilder(args).Build();
            try
            {
                Log.Information("Starting web host");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseContentRoot(AppPath);
                });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",optional:false,reloadOnChange:true)
            // .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")??"Production"}.json",optional:true)
            .AddEnvironmentVariables()
            .Build();
            return builder;
        }

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
            
            .Enrich.WithProperty("Application", AppName)
            .Enrich.WithProperty("Environment", "Production")
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }
    }
}
