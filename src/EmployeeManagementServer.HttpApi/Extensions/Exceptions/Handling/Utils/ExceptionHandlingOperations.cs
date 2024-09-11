using System.Text;
using System.Text.Json;
using EmployeeManagementServer.Domain.Shared.Constants.Exceptions.Global.Codes;
using EmployeeManagementServer.Domain.Shared.Constants.Exceptions.Global.Messages;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils.Global;
using FluentValidation;
using Serilog;
using Serilog.Events;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils;

/// <summary>
/// Provides utility methods for handling exceptions, including logging and formatting exception messages.
/// </summary>
public static class ExceptionHandlingOperations
{
    /// <summary>
    /// Generates a JSON response string based on the provided exception.
    /// This method formats the exception details into a JSON structure that includes error codes and messages.
    /// The response varies depending on the type of exception.
    /// </summary>
    /// <param name="exception">The exception for which the response is being generated.</param>
    /// <returns>A JSON-formatted string representing the exception details.</returns>
    public static string GetExecutedResponse(Exception exception)
    {
        string responsibleExceptionCode =
                ((exception is BusinessException customException
                     ? customException.Code ?? AppSpecificCodes.InternalServerCode
                     : AppSpecificCodes.InternalServerCode) +
                 $" [For more details on system codes, visit: {AboutAppSpecificCodes.SourceLink}]"); /*
                 * Determines the appropriate exception code to use for the response.
                 * If the exception is a BusinessException, use its Code property or fall back to InternalServerCode if the Code is null.
                 * Otherwise, use the InternalServerCode by default.
                 */

        string responsibleExceptionMessage = exception is UserFriendlyException userFriendlyException
                ? userFriendlyException.Message +
                  (!string.IsNullOrWhiteSpace(userFriendlyException.Details)
                      ? $" (Details: {userFriendlyException.Details})"
                      : string.Empty)
                : AppSpecificMessages.InternalExceptionMessage; /*
                                                                 * Constructs the exception message to include in the response.
                                                                 * If the exception is a UserFriendlyException, use its Message property and append Details if available.
                                                                 * Otherwise, use a default internal exception message.
                                                                 */

        /* If the exception is a ValidationException, serialize a JSON object with the type, code, and error messages. */
        if (exception.GetType() == typeof(ValidationException))
            return JsonSerializer.Serialize(new
            {
                Type = AboutAppSpecificCodes.SourceType,
                Code = responsibleExceptionCode,
                Errors = $"Message: {((ValidationException)exception).Errors.Select(selector => selector.ErrorMessage)}"
            });

        /* For other types of exceptions, serialize a JSON object with the type, code, and formatted exception message. */
        return JsonSerializer.Serialize(new
        {
            Type = AboutAppSpecificCodes.SourceType,
            Code = responsibleExceptionCode,
            Errors = $"Message: {responsibleExceptionMessage}"
        });
    }

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