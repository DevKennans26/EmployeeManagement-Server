using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using Serilog.Events;

namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Utils;

/// <summary>
/// Utility class for converting custom logging levels to Serilog's LogEventLevel.
/// </summary>
public static class LogLevelConverter
{
    /// <summary>
    /// Converts the custom LoggingLevels enum to Serilog's LogEventLevel.
    /// </summary>
    /// <param name="level">The custom logging level to be converted.</param>
    /// <returns>The equivalent Serilog LogEventLevel.</returns>
    public static LogEventLevel ConvertEnumToSerilogEventLevel(LoggingLevels level) =>
        level switch
        {
            LoggingLevels.Trace => LogEventLevel.Verbose,
            LoggingLevels.Debug => LogEventLevel.Debug,
            LoggingLevels.Information => LogEventLevel.Information,
            LoggingLevels.Warning => LogEventLevel.Warning,
            LoggingLevels.Error => LogEventLevel.Error,
            LoggingLevels.Critical => LogEventLevel.Fatal
        };
}