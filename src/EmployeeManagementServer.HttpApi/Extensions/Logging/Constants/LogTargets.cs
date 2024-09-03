namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

/// <summary>
/// Enum for specifying the log targets that can be enabled.
/// </summary>
[Flags]
public enum LogTargets
{
    /// <summary>
    /// Determines if console logging is enabled.
    /// Console logging is default enabled on system, mean: it is suggested that enable the console option.
    /// </summary>
    Console = 0,

    /// <summary>
    /// Determines if logging to a Seq server is enabled. To enable this, ensure that Seq is running on the specified server and port.
    /// </summary>
    /// <remarks>
    /// To set up Seq, refer to the following resource: https://hub.docker.com/r/datalust/seq
    /// </remarks>
    Seq = 1
}