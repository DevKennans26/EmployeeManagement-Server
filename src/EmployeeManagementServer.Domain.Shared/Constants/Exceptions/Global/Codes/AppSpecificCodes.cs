namespace EmployeeManagementServer.Domain.Shared.Constants.Exceptions.Global.Codes;

/// <summary>
/// Stores all unique application-specific codes used to identify various exceptions in the system.
/// Each exception code has a structured format that helps pinpoint the origin of the error, making debugging, logging, and monitoring much easier and more organized.
/// This class acts as a central place where exception codes are defined and maintained, ensuring consistency across the system.
/// In <see cref="AboutAppSpecificCodes"/>, we can see all metadata informations about this. 
/// </summary>
public static class AppSpecificCodes
{
    /* Code Structure:
     * - **EMS**: Project acronym (Employee Management Server)
     * - **01**: The version of the project where the error occurred
     * - **00**: Indicates the layer or module (default layer in this case)
     * - **01**: The specific exception number within the layer.
     */

    /// <summary>
    /// The default code representing an internal server error in the application.
    /// This code is typically used for general or unhandled server exceptions.
    /// When this error code is triggered, it signals that something unexpected occurred within the backend, and requires investigation.
    /// </summary>
    public static string InternalServerCode { get; } = "EMS:01:00:01";
}