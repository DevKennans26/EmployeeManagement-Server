using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Utils;
using Serilog;

namespace EmployeeManagementServer.HttpApi.Extensions.Logging;

/// <summary>
/// Provides logging configuration services using Serilog.
/// </summary>
public static class LogService
{
    /// <summary>
    /// Configures Serilog based on the provided logging options and appsettings.json configuration.
    /// </summary>
    /// <param name="configuration">The configuration object containing logging settings.</param>
    public static void ConfigureLogging(IConfiguration configuration)
    {
        var logLevelString = configuration[LoggingDefaults.DefaultLogLevel];
        if (!Enum.TryParse<LoggingLevels>(logLevelString, true, out var logLevel))
            logLevel = LoggingLevels.Information; /* Default to Information if not specified */

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Is(LogLevelConverter.ConvertEnumToSerilogEventLevel(logLevel))
            .WriteTo.Console(); /* LoggingDefaults.IsConsoleLoggingEnabled is true */
        
        if (LoggingDefaults.IsConsoleLoggingEnabled)
            loggerConfig.WriteTo.Console();
        
        Log.Logger = loggerConfig.CreateLogger();
    }
}