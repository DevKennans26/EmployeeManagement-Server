namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling;

/// <summary>
/// Static class responsible for registering and configuring middleware components related to global exception handling.
/// This class provides a centralized mechanism to handle exceptions that may occur during the application's lifecycle.
/// By using this class, we ensure that all unhandled exceptions are captured and properly processed, preventing the application from crashing unexpectedly.
/// </summary>
public static class RegisterExceptionHandlerMiddleware
{
    /// <summary>
    /// Configures the global exception handling middleware in the request pipeline.
    /// This method registers a custom middleware (<see cref="ExceptionHandlerMiddleware"/>), which intercepts all exceptions
    /// that occur during the request lifecycle. It logs these exceptions and generates a standardized error response.
    /// </summary>
    /// <param name="applicationBuilder">
    /// The <see cref="IApplicationBuilder"/> instance used to configure the application's request pipeline.
    /// This is where the middleware gets registered.
    /// </param>
    /// <remarks>
    /// It is recommended to register this middleware early in the request pipeline to ensure that all exceptions, including those from authentication and routing, are captured and handled properly. Place it before any middleware that might throw exceptions, such as routing or authentication.
    /// </remarks>
    public static void ConfigureGlobalExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}