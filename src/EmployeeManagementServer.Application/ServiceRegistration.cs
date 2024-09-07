using System.Globalization;
using System.Reflection;
using EmployeeManagementServer.Application.Behaviors.Validating;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementServer.Application;

/// <summary>
/// This static class registers application-wide services on current layer: '.Application'
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Extension method for IServiceCollection to add application-specific services.
    /// </summary>
    /// <param name="services">The IServiceCollection where services will be registered.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        Assembly[]
            assemblies = AppDomain.CurrentDomain.GetAssemblies(); /* Get all assemblies in the current AppDomain */

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assemblies);
        }); /* Register MediatR handlers from all discovered assemblies. */

        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(FluentValidationBehavior<,>)); /* Add a pipeline behavior that integrates FluentValidation into MediatR's pipeline. */

        services.AddValidatorsFromAssemblies(assemblies); /* Register all validators found in the assemblies. */
        ValidatorOptions.Global.LanguageManager.Culture =
            new CultureInfo("en-US"); /* Set global validation culture to "en-US". */
    }
}