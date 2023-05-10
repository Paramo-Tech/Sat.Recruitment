using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                _logger.Info("App init");
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "App failed to init");
            }
            finally{
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(log =>
                {
                    log.ClearProviders();
                    log.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                });
    }
}
