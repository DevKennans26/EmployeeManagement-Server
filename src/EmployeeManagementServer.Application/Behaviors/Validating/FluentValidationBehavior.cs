using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace EmployeeManagementServer.Application.Behaviors.Validating;

/// <summary>
/// A MediatR pipeline behavior that integrates FluentValidation.
/// It validates incoming requests before passing them to the next handler.
/// </summary>
/// <typeparam name="TRequest">The request type to be validated.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Constructor to inject all validators for the specific request type.
    /// </summary>
    /// <param name="validators">A collection of validators for the TRequest type.</param>
    public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Handles the request validation. If validation succeeds, it forwards the request to the next handler.
    /// If validation fails, a ValidationException is thrown.
    /// </summary>
    /// <param name="request">The incoming request object to be validated.</param>
    /// <param name="next">The next handler in the MediatR pipeline.</param>
    /// <param name="cancellationToken">Cancellation token to stop the process if requested.</param>
    /// <returns>The task that represents the request processing, potentially throwing validation exceptions.</returns>
    /// <exception cref="ValidationException">Thrown if any validation errors are found.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationContext<TRequest>
            validationContext =
                new ValidationContext<TRequest>(request); /* Create a validation context from the incoming request. */
        /* Collect validation failures, if any: */
        List<ValidationFailure> failures = _validators
                .Select(validator => validator.Validate(validationContext))
                .SelectMany(result => result.Errors)
                .GroupBy(element => element.ErrorMessage)
                .Select(element => element.First())
                .Where(field => field is not null)
                .ToList(); /*
                            * Grouping to avoid duplicate errors.
                            * Take the first unique error.
                            */

        if (failures.Any()) /* If there are validation errors, throw a ValidationException. */
            throw new ValidationException(failures);

        return await next();
    }
}