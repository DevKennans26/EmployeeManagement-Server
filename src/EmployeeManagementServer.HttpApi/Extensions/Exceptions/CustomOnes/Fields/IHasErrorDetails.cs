namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes.Fields;

/// <summary>
/// Interface indicating that an exception has additional error details.
/// </summary>
public interface IHasErrorDetails
{
    /// <summary>
    /// Gets the detailed information about the error.
    /// </summary>
    public string? Details { get; }
}