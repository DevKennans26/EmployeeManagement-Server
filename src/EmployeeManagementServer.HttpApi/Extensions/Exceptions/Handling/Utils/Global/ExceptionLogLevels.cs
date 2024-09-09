using EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Utils;
using Serilog.Events;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils.Global;

/// <summary>
/// Provides methods for determining the appropriate Serilog log level based on the type of exception.
/// </summary>
public static class ExceptionLogLevels
{
    /// <summary>
    /// Retrieves the Serilog log level corresponding to the provided exception.
    /// Maps specific exception types to their respective log levels.
    /// </summary>
    /// <param name="exception">The exception for which the log level is determined.</param>
    /// <returns>The Serilog log level.</returns>
    public static LogEventLevel GetLoggingLevel(Exception exception) =>
        exception switch
        {
            UserFriendlyException userFriendlyException =>
                LogLevelConverter.ConvertEnumToSerilogEventLevel(userFriendlyException.LogLevel),
            BusinessException businessException =>
                LogLevelConverter.ConvertEnumToSerilogEventLevel(businessException.LogLevel),

            _ => LogEventLevel.Error
        };
}