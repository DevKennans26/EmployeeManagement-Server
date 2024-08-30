namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

/// <summary>
/// Enum for specifying the different logging levels that can be used in the application.
/// </summary>
public enum LoggingLevels
{
    /// <summary>
    /// Logs that contain the most detailed messages. Typically, these messages are only useful during development.
    /// </summary>
    Trace = 0,

    /// <summary>
    /// Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging.
    /// </summary>
    Debug = 1,

    /// <summary>
    /// Logs that track the general flow of the application. These logs should have long-term value.
    /// </summary>
    Information = 2,

    /// <summary>
    /// Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application to stop.
    /// </summary>
    Warning = 3,

    /// <summary>
    /// Logs that highlight when the current flow of execution is stopped due to a failure.
    /// </summary>
    Error = 4,

    /// <summary>
    /// Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.
    /// </summary>
    Critical = 5
}