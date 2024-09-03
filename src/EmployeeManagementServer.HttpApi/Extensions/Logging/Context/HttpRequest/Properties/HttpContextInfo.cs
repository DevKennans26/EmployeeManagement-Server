namespace EmployeeManagementServer.HttpApi.Extensions.Logging.Context.HttpRequest.Properties;

/// <summary>
/// Contains information about the HTTP context, including IP address, host, protocol, scheme, and user identity.
/// </summary>
public class HttpContextInfo
{
    /// <summary>
    /// Gets or sets the client's IP address.
    /// </summary>
    public string IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the host name of the request.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the protocol used for the request (e.g., HTTP/1.1).
    /// </summary>
    public string Protocol { get; set; }

    /// <summary>
    /// Gets or sets the scheme of the request (e.g., http or https).
    /// </summary>
    public string Scheme { get; set; }

    /// <summary>
    /// Gets or sets the user identity making the request, if authenticated.
    /// </summary>
    public string User { get; set; }
}