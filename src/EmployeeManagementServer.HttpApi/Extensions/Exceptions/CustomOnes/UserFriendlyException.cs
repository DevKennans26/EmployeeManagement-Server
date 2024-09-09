using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes;

/// <summary>
/// This exception type is directly shown to the user.
/// </summary>
public class UserFriendlyException : BusinessException,
    IUserFriendlyException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserFriendlyException"/> class with a specified error message, error code, error details, and logging level.
    /// </summary>
    /// <param name="message">The message that describes the error. This will be passed to the base class.</param>
    /// <param name="code">The error code associated with this exception. Default is null.</param>
    /// <param name="details">Additional error details. Default is null.</param>
    /// <param name="innerException">The exception that is the cause of the current exception. Default is null.</param>
    /// <param name="logLevel">The logging level for this exception. Default is <see cref="LoggingLevels.Warning"/>.</param>
    public UserFriendlyException(
        string message,
        string? code = null,
        string? details = null,
        Exception? innerException = null,
        LoggingLevels logLevel = LoggingLevels.Warning)
        : base(
            code,
            message,
            details,
            innerException,
            logLevel)
    {
        Details = details;
    }
}