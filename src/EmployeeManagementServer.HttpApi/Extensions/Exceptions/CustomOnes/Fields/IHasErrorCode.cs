namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes.Fields;

/// <summary>
/// Interface indicating that an exception has an associated error code.
/// </summary>
public interface IHasErrorCode
{
    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string? Code { get; }
}