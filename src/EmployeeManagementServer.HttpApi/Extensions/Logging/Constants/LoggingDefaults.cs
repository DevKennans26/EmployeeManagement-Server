namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

/// <summary>
/// Constants for default logging settings. These settings dictate the default behavior of the logging system.
/// </summary>
public static class LoggingDefaults
{
    #region Unchangeable

    /// <summary>
    /// Determines if console logging is enabled. This is a system setting and cannot be changed.
    /// </summary>
    public static bool IsConsoleLoggingEnabled { get; } = true;

    /// <summary>
    /// The default logging level configuration key. This points to the location in the configuration where the log level is defined.
    /// </summary>
    public static string DefaultLogLevel { get; } = "Logging:Settings:LogLevel:Default";

    #endregion

    /// <summary>
    /// Determines if logging to the database is enabled. To enable this, ensure that the database specified in the configuration exists.
    /// </summary>
    public static bool IsDatabaseLoggingEnabled { get; set; } = false;

    /// <summary>
    /// Determines if logging to a Seq server is enabled. To enable this, ensure that Seq is running on the specified server and port.
    /// </summary>
    /// <remarks>
    /// To set up Seq, refer to the following resource: https://hub.docker.com/r/datalust/seq
    /// </remarks>
    public static bool IsSeqLoggingEnabled { get; set; } = false;
}