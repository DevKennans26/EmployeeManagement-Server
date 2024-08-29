namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

/// <summary>
/// Constants for default logging settings.
/// </summary>
public static class LoggingDefaults
{
    #region Unchangeable

    public static bool IsConsoleLoggingEnabled { get; } = true;

    public static string DefaultLogLevel { get; } = "Logging:LogLevel:Default";

    #endregion
}