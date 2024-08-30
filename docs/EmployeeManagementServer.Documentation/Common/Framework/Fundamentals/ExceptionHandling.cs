namespace EmployeeManagementServer.Documentation.Common.Framework.Fundamentals;

public class ExceptionHandling
{
    #region ABP Documentation

    /* https://abp.io/docs/latest/framework/fundamentals/exception-handling */
    
    /*
     * ABP provides a built-in infrastructure and offers a standard model for handling exceptions.
     *
     * Automatically handles all exceptions and sends a standard formatted error message to the client for an API/AJAX request.
     * Automatically hides internal infrastructure errors and returns a standard error message.
     * Provides an easy and configurable way to localize exception messages.
     * Automatically maps standard exceptions to HTTP status codes and provides a configurable option to map custom exceptions.
     */

    #region Automatic Exception Handling

    /*
     * AbpExceptionFilter handles an exception if any of the following conditions are met:
     *
     * Exception is thrown by a controller action which returns an object result (not a view result).
     * The request is an AJAX request (X-Requested-With HTTP header value is XMLHttpRequest).
     * Client explicitly accepts the application/json content type (via accept HTTP header).
     *
     * If the exception is handled it's automatically logged and a formatted JSON message is returned to the client.
     */

    #endregion

    #region Error Message Format

    /* Error Message is an instance of the RemoteServiceErrorResponse class. The simplest error JSON has a message property as shown below: */
    
    // {
    //     "error": {
    //         "message": "This topic is locked and can not add a new message"
    //     }
    // }
    
    /* There are optional fields those can be filled based upon the exception that has occurred. */

    #region Error Code

    /* Error code is an optional and unique string value for the exception. Thrown Exception should implement the IHasErrorCode interface to fill this field. Example JSON value: */
    
    // {
    //     "error": {
    //         "code": "App:010042",
    //         "message": "This topic is locked and can not add a new message"
    //     }
    // }
    
    /* Error code can also be used to localize the exception and customize the HTTP status code (see the related sections below). */

    #endregion

    #region Error Details

    /* Error details in an optional field of the JSON error message. Thrown Exception should implement the IHasErrorDetails interface to fill this field. Example JSON value: */
    
    // {
    //     "error": {
    //         "code": "App:010042",
    //         "message": "This topic is locked and can not add a new message",
    //         "details": "A more detailed info about the error..."
    //     }
    // }

    #endregion

    #region Validation Errors

    /* validationErrors is a standard field that is filled if the thrown exception implements the IHasValidationErrors interface. */
    
    // {
    //     "error": {
    //         "code": "App:010046",
    //         "message": "Your request is not valid, please correct and try again!",
    //         "validationErrors": [{
    //             "message": "Username should be minimum length of 3.",
    //             "members": ["userName"]
    //         },
    //         {
    //             "message": "Password is required",
    //             "members": ["password"]
    //         }]
    //     }
    // }
    
    /* AbpValidationException implements the IHasValidationErrors interface and it is automatically thrown by the framework when a request input is not valid. So, usually you don't need to deal with validation errors unless you have higly customised validation logic. */

    #endregion

    #endregion

    #region Logging

    /* Caught exceptions are automatically logged. */

    #region Log Level

    /* Exceptions are logged with the Error level by default. The Log level can be determined by the exception if it implements the IHasLogLevel interface. Example: */
    
    // public class MyException : Exception, IHasLogLevel
    // {
    //     public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    //
    //     //...
    // }

    #endregion

    #region Self Logging Exceptions

    /* Some exception types may need to write additional logs. They can implement the IExceptionWithSelfLogging if needed. Example: */
    
    // public class MyException : Exception, IExceptionWithSelfLogging
    // {
    //     public void Log(ILogger logger)
    //     {
    //         //...log additional info
    //     }
    // }

    /* ILogger.LogException extension methods is used to write exception logs. You can use the same extension method when needed. */
    
    #endregion

    #endregion

    #region Business Exceptions

    /*
     * Most of your own exceptions will be business exceptions. The IBusinessException interface is used to mark an exception as a business exception.
     *
     * BusinessException implements the IBusinessException interface in addition to the IHasErrorCode, IHasErrorDetails and IHasLogLevel interfaces. The default log level is Warning.
     */
    /* Usually you have an error code related to a particular business exception. For example: */
    
    // throw new BusinessException(QaErrorCodes.CanNotVoteYourOwnAnswer);
    
    /*
     * QaErrorCodes.CanNotVoteYourOwnAnswer is just a const string. The following error code format is recommended:
     * <code-namespace>:<error-code>
     * code-namespace is a unique value specific to your module/application. Example:
     * Volo.Qa:010002
     * Volo.Qa is the code-namespace here. code-namespace is then will be used while localizing exception messages.
     *
     * You can directly throw a BusinessException or derive your own exception types from it when needed.
     * All properties are optional for the BusinessException class. But you generally set either ErrorCode or Message property.
     */

    #endregion

    #region User Friendly Exception

    /*
     * If an exception implements the IUserFriendlyException interface, then ABP does not change it's Message and Details properties and directly send it to the client.
     */
    
    /* UserFriendlyException class is the built-in implementation of the IUserFriendlyException interface. Example usage: */
    
    // throw new UserFriendlyException(
    //  "Username should be unique!"
    // );

    #endregion

    #region HTTP Status Code Mapping

    /*
     * ABP tries to automatically determine the most suitable HTTP status code for common exception types by following these rules:
     * 
     * For the AbpAuthorizationException:
     * Returns 401 (unauthorized) if user has not logged in.
     * Returns 403 (forbidden) if user has logged in.
     *
     * Returns 400 (bad request) for the AbpValidationException.
     * Returns 404 (not found) for the EntityNotFoundException.
     * Returns 403 (forbidden) for the IBusinessException (and IUserFriendlyException since it extends the IBusinessException).
     * Returns 501 (not implemented) for the NotImplementedException.
     * Returns 500 (internal server error) for other exceptions (those are assumed as infrastructure exceptions).
     */
    
    /* The IHttpExceptionStatusCodeFinder is used to automatically determine the HTTP status code. The default implementation is the DefaultHttpExceptionStatusCodeFinder class. It can be replaced or extended as needed. */

    #endregion
    
    #endregion
}