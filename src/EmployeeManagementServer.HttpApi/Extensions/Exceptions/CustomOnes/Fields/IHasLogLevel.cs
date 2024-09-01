using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes.Fields;

/// <summary>
/// Interface to define a <see cref="LogLevel"/> property (see <see cref="LogLevel"/>).
/// </summary>
public interface IHasLogLevel
{
    /// <summary>
    /// Log severity.
    /// </summary>
    public LoggingLevels LogLevel { get; set; }
}