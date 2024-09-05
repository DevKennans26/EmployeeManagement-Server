using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Utils;
using Serilog;
using Serilog.Exceptions;

namespace EmployeeManagementServer.HttpApi.Extensions.Logging;

/// <summary>
/// Provides logging configuration services using Serilog, based on the application's configuration settings.
/// Console logging is default enabled on system.
/// </summary>
public static class LogService
{
    /// <summary>
    /// Configures Serilog based on the provided logging options in the appsettings.json configuration.
    /// This method sets up different logging sinks (e.g., Console, Seq) based on the settings.
    /// Additionally, if <paramref name="isExceptionDetailsEnabled"/> is set to true, detailed exception information
    /// will be included in the logs (this feature enriches log entries with full exception details).
    /// </summary>
    /// <param name="configuration">The configuration object containing logging settings.</param>
    /// <param name="isExceptionDetailsEnabled">If true, detailed exception information will be added to the logs. Defaults to false.</param>
    /// <param name="logTargets">The log targets selected by the user. Console logging is always enabled by default on the system.</param>
    public static void ConfigureLogging(IConfiguration configuration, bool isExceptionDetailsEnabled = false,
        LogTargets logTargets = LogTargets.Console)
    {
        if (!logTargets.HasFlag(LogTargets.Console))
            logTargets |= LogTargets.Console; /* Console logging is always enabled and cannot be disabled on system. */

        /* Retrieve the log level from the configuration */
        var logLevelString = configuration["Logging:Settings:LogLevel:Default"]!;
        if (!Enum.TryParse<LoggingLevels>(logLevelString, true, out var logLevel))
            logLevel = LoggingLevels.Information; /* Default to Information if not specified */

        var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(LogLevelConverter.ConvertEnumToSerilogEventLevel(logLevel))
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName(); /*
                                            * Enriches logs with detailed exception information (optional), contextual information from the log context, the current environment name, the machine name where the application is running.
                                            */

        /* Check if advanced exception details option is enabled
         * Adds detailed exception information to the log entries if enabled.
         * This is useful for debugging, as it provides additional context about exceptions.
         */
        if (isExceptionDetailsEnabled)
            loggerConfig.Enrich.WithExceptionDetails();

        /* Console logging is always enabled by default on the system, but it is recommended to explicitly include the Console log target for clarity */
        loggerConfig.WriteTo.Console();

        /* Check if Seq logging is enabled */
        if (logTargets.HasFlag(LogTargets.Seq))
            loggerConfig.WriteTo.Seq(serverUrl: configuration.GetValue<string>("Logging:Settings:Seq:ServerUrl")!);

        Log.Logger = loggerConfig.CreateLogger();
    }
}