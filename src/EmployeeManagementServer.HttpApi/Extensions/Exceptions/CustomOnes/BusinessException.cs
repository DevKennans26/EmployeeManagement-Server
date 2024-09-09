using System.Runtime.CompilerServices;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes.Fields;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes;

/// <summary>
/// Represents an exception that occurs within the business logic of the application.
/// </summary>
public class BusinessException : Exception,
    IBusinessException,
    IHasErrorCode,
    IHasErrorDetails,
    IHasLogLevel
{
    /// <inheritdoc/>
    public string? Code { get; set; }

    /// <inheritdoc/>
    public string? Details { get; set; }

    /// <inheritdoc/>
    public LoggingLevels LogLevel { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessException"/> class with a specified error message, error code, error details, and logging level.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="code">The error code associated with this exception. Default is null.</param>
    /// <param name="details">Additional error details. Default is null.</param>
    /// <param name="innerException">The exception that is the cause of the current exception. Default is null.</param>
    /// <param name="logLevel">The logging level for this exception. Default is <see cref="LoggingLevels.Warning"/>.</param>
    public BusinessException(
        string? code = null,
        string? message = null,
        string? details = null,
        Exception? innerException = null,
        LoggingLevels logLevel = LoggingLevels.Warning,
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0,
        [CallerMemberName] string memberName = "")
        : base(
            message ?? $"An exception occurred in {sourceFilePath} at line {sourceLineNumber}, in method {memberName}.",
            innerException)
    {
        Code = code;
        Details = details;
        LogLevel = logLevel;
    }

    public BusinessException WithData(string name, object value)
    {
        Data[name] = value;
        return this;
    }
}