using System.Text;
using EmployeeManagementServer.Domain.Shared.Constants.Exceptions.Global.Codes;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils.Global;
using Serilog;
using Serilog.Events;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils;

/// <summary>
/// Provides utility methods for handling exceptions, including logging and formatting exception messages.
/// </summary>
public static class ExceptionHandlingOperations
{
    /// <summary>
    /// Logs the given exception asynchronously using Serilog.
    /// This method offloads the logging task to a background thread to avoid blocking the main execution flow.
    /// </summary>
    /// <param name="exception">The exception to be logged.</param>
    public static void LogException(Exception exception)
    {
        Task.Run(() =>
        {
            LogEventLevel logLevel = ExceptionLogLevels.GetLoggingLevel(exception);
            Log.Logger.Write(level: logLevel,
                messageTemplate: GetExceptionMessageTemplate(exception, logLevel.ToString()));
        });
    }

    /// <summary>
    /// Constructs a formatted string for logging the exception details.
    /// Includes the exception message, inner exception details, and additional information based on the exception type.
    /// </summary>
    /// <param name="exception">The exception for which the message template is constructed.</param>
    /// <param name="logLevel">The log level associated with the exception.</param>
    /// <returns>A formatted string representing the exception message template.</returns>
    private static string GetExceptionMessageTemplate(Exception exception, string logLevel)
    {
        StringBuilder messageBuilder = new StringBuilder();

        messageBuilder.AppendLine("Handled a global exception with the following informations:")
            .AppendLine($"Message: {exception.Message}");

        AppendInnerExceptionsMessages(exception, messageBuilder);

        if (exception is BusinessException customException)
        {
            messageBuilder.AppendLine(
                $"Code: {customException.Code ?? AppSpecificCodes.InternalServerCode} " +
                $"[For more details on system codes, visit: {AboutAppSpecificCodes.SourceLink}]");

            if (!string.IsNullOrWhiteSpace(customException.Details))
                messageBuilder.AppendLine($"Details: {customException.Details}");
        }
        else
            messageBuilder.AppendLine(
                $"Code: {AppSpecificCodes.InternalServerCode} " +
                $"[For more details on system codes, visit: {AboutAppSpecificCodes.SourceLink}]");

        messageBuilder.AppendLine($"Log level: {logLevel} [Serilog]");

        return messageBuilder.ToString();
    }

    /// <summary>
    /// Appends details of inner exceptions to the message builder.
    /// Each inner exception is annotated with its level in the exception hierarchy.
    /// </summary>
    /// <param name="exception">The exception which inner exceptions will be appended.</param>
    /// <param name="messageBuilder">The StringBuilder to which the inner exception details will be appended.</param>
    private static void AppendInnerExceptionsMessages(Exception exception, StringBuilder messageBuilder)
    {
        Exception? innerException = exception.InnerException;
        int level = 1;

        while (innerException is not null)
        {
            messageBuilder.AppendLine($"Inner exception message (level {level}): {innerException.Message}");

            innerException = innerException.InnerException;
            level++;
        }
    }
}