using System.Data;
using System.Security;
using System.Security.Authentication;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.CustomOnes;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils.Global;

/// <summary>
/// Maps exceptions to their corresponding HTTP status codes.
/// Provides a method to determine the appropriate status code based on the type of exception encountered.
/// </summary>
public static class ExceptionStatusCodes
{
    /// <summary>
    /// Retrieves the HTTP status code corresponding to the provided exception.
    /// Maps specific exception types to standard HTTP status codes.
    /// </summary>
    /// <param name="exception">The exception for which the status code is determined.</param>
    /// <returns>The HTTP status code.</returns>
    public static int GetStatusCode(Exception exception) =>
        exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            AuthenticationException => StatusCodes.Status401Unauthorized,
            SecurityException => StatusCodes.Status403Forbidden,
            SecurityTokenException => StatusCodes.Status403Forbidden,

            ArgumentException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status422UnprocessableEntity,

            FileNotFoundException => StatusCodes.Status404NotFound,
            DirectoryNotFoundException => StatusCodes.Status404NotFound,
            KeyNotFoundException => StatusCodes.Status404NotFound,

            TaskCanceledException => StatusCodes.Status408RequestTimeout,
            OperationCanceledException => StatusCodes.Status408RequestTimeout,
            TimeoutException => StatusCodes.Status408RequestTimeout,

            DuplicateNameException => StatusCodes.Status409Conflict,
            InvalidOperationException => StatusCodes.Status500InternalServerError,
            NotImplementedException => StatusCodes.Status501NotImplemented,

            HttpRequestException => StatusCodes.Status503ServiceUnavailable,

            UserFriendlyException => StatusCodes.Status400BadRequest,
            BusinessException => StatusCodes.Status500InternalServerError,

            _ => StatusCodes.Status500InternalServerError
        };
}