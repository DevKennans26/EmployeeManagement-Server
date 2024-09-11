namespace EmployeeManagementServer.Domain.Shared.Constants.Exceptions.Global.Codes;

/// <summary>
/// Contains information and metadata related to application-specific codes used throughout the system.
/// This class is primarily used for storing general references, like source code links, that assist in managing and maintaining app-specific exception codes.
/// Think of this as the reference point for where and how to find detailed information about exception codes.
/// </summary>
public static class AboutAppSpecificCodes
{
    /// <summary>
    /// A URL pointing to the source code repository that defines and manages application-specific codes.
    /// Useful for developers who need to look up or update the code definitions directly.
    /// This link serves as a quick navigation tool for locating the code base related to exceptions.
    /// </summary>
    public static string SourceLink { get; } =
        "https://github.com/DevKennans26/EmployeeManagement-Server/tree/main/src/EmployeeManagementServer.Domain.Shared/Constants/Exceptions/Global/Codes/AppSpecificCodes.cs";

    public static string SourceType { get; } = "https://datatracker.ietf.org/doc/html/rfc7231";
}