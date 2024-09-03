using System.Security.Claims;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Context.HttpRequest.Properties;
using Serilog;

namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Context.HttpRequest;

/// <summary>
/// Provides methods to enrich Serilog logs with additional (custom) HTTP request context information.
/// </summary>
public static class Enricher
{
    /// <summary>
    /// Enriches the diagnostic context with HTTP request information, such as protocol, scheme, IP address, host, and user.
    /// </summary>
    /// <param name="diagnosticContext">The diagnostic context used by Serilog.</param>
    /// <param name="httpContext">The HTTP context containing the request data.</param>
    internal static void HttpRequestEnricher(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var httpContextInfo = new HttpContextInfo
        {
            Protocol = httpContext.Request.Protocol,
            Scheme = httpContext.Request.Scheme,
            IpAddress = httpContext.Connection.RemoteIpAddress.ToString(),
            Host = httpContext.Request.Host.ToString(),
            User = GetUserInfo(httpContext.User)
        };

        diagnosticContext.Set("HttpContext", httpContextInfo,
            true); /* Adds the HttpContext information to the log context. */
    }

    /// <summary>
    /// Retrieves the user information from the given claims principal.
    /// </summary>
    /// <param name="user">The claims principal representing the authenticated user.</param>
    /// <returns>The username if the user is authenticated; otherwise, the machine's environment username.</returns>
    private static string GetUserInfo(ClaimsPrincipal user)
    {
        if (user.Identity != null && user.Identity.IsAuthenticated)
            return user.Identity.Name;

        return Environment.UserName;
    }
}