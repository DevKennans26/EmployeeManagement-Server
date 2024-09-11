using System.Net.Mime;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils;
using EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling.Utils.Global;

namespace EmployeeManagementServer.HttpApi.Extensions.Exceptions.Handling;

/// <summary>
/// Middleware to handle exceptions globally in the application.
/// Catches exceptions thrown by subsequent middleware and provides a standardized response.
/// </summary>
public class ExceptionHandlerMiddleware : IMiddleware
{
    /// <summary>
    /// Executes the middleware, handling any exceptions thrown by the next middleware in the pipeline.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <param name="next">The delegate to the next middleware in the pipeline.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context); /* Pass control to the next middleware in the pipeline. */
        }
        catch (Exception exc)
        {
            await HandleExceptionAsync(context, exc); /* Handle the exception and generate a response. */
        }
    }

    /// <summary>
    /// Handles the exception by logging it and writing an appropriate response to the HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context for the current request.</param>
    /// <param name="exception">The exception that was thrown.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        Task.Run(() => ExceptionHandlingOperations.LogException(exception)); /*
                                                                              * The thrown exception is formatted with its values and thrown into the log.
                                                                              */

        httpContext.Response.ContentType = MediaTypeNames.Application.Json; /* Set the response content type to JSON. */
        httpContext.Response.StatusCode =
            ExceptionStatusCodes.GetStatusCode(exception); /* Set the HTTP status code based on the exception type. */

        /* This method generates a JSON response body containing details about the exception. */
        string ExecutedExceptionResponse = ExceptionHandlingOperations.GetExecutedResponse(exception);
        /* Writes the JSON-formatted exception response to the HTTP context's response body asynchronously. */
        await httpContext.Response.WriteAsync(ExecutedExceptionResponse);
    }
}