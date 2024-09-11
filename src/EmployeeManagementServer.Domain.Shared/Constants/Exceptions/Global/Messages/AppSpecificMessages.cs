namespace EmployeeManagementServer.Domain.Shared.Constants.Exceptions.Global.Messages;

/// <summary>
/// Provides a collection of predefined messages used across the application for consistent error reporting.
/// This class stores all general messages that might be shown to users when exceptions occur.
/// It centralizes message definitions to avoid redundancy and ensure all user-facing messages are standardized.
/// </summary>
public static class AppSpecificMessages
{
    /// <summary>
    /// The default message shown when an unexpected internal server error occurs.
    /// This message is generic and user-friendly, informing users that an error has occurred without revealing sensitive details.
    /// It's primarily used in cases where no specific or custom error message is available.
    /// </summary>
    public static string InternalExceptionMessage { get; } =
        "An unexpected error was encountered on the internal server, please try again later.";
}