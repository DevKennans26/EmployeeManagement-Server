using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using Serilog.Events;

namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Utils;

/// <summary>
/// Utility class for converting custom logging levels defined in the application to Serilog's corresponding <see cref="LogEventLevel"/>.
/// </summary>
public static class LogLevelConverter
{
    /// <summary>
    /// Converts the custom <see cref="LoggingLevels"/> enum to Serilog's <see cref="LogEventLevel"/>.
    /// </summary>
    /// <param name="level">The custom logging level to be converted.</param>
    /// <returns>The equivalent <see cref="LogEventLevel"/> that Serilog can use.</returns>
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