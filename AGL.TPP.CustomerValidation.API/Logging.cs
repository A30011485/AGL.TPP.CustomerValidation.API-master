using System;
using AGL.TPP.CustomerValidation.API.Config;
using AGL.TPP.CustomerValidation.API.Models;
using Destructurama;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace AGL.TPP.CustomerValidation.API
{
    /// <summary>
    /// Logging class
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Gets the default logger to log
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        /// <returns>Returns an instance of ILogger</returns>
        public static ILogger GetLogger(IConfiguration configuration)
        {
            var loggingOptions = configuration.GetSection("Logging").Get<LoggingOptions>();
            var appConfiguration = configuration.GetSection("AppConfiguration").Get<AppConfiguration>();

            if (!Enum.TryParse<LogEventLevel>(loggingOptions.Trace.LogLevel, out var logLevel))
            {
                throw new Exception("Log Level does not match one of the types");
            }

            if (!Enum.TryParse<LogEventLevel>(loggingOptions.LogLevel, out var minimumEventLevel))
            {
                throw new Exception("Log Level does not match one of the types");
            }

            var loggerConfiguration = new LoggerConfiguration()
                .Destructure.UsingAttributes()
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(minimumEventLevel))
                .MinimumLevel.Override("Microsoft", minimumEventLevel)
                .Enrich.FromLogContext()
                .Enrich.WithProperty(nameof(Environment.MachineName), Environment.MachineName)
                .Enrich.WithProperty(nameof(appConfiguration.ApplicationIdentifier),
                    appConfiguration.ApplicationIdentifier)
                .Enrich.WithProperty(nameof(appConfiguration.Environment), appConfiguration.Environment);

            if (loggingOptions.Trace.Enabled)
            {
                loggerConfiguration.WriteTo.Trace(
                    logLevel,
                    loggingOptions.LogOutputTemplate
                );

                loggerConfiguration.WriteTo.AzureApp();
            }
            if (loggingOptions.Splunk.Enabled)
            {
                Enum.TryParse(loggingOptions.Splunk.LogLevel, false, out LogEventLevel splunkLogEventLevel);

                loggerConfiguration.WriteTo.EventCollector(
                    loggingOptions.Splunk.Host,
                    loggingOptions.Splunk.Token,
                    restrictedToMinimumLevel: splunkLogEventLevel,
                    outputTemplate: loggingOptions.LogOutputTemplate
                );
            }
            //add logs to blob
            return loggerConfiguration.CreateLogger();
        }
    }
}