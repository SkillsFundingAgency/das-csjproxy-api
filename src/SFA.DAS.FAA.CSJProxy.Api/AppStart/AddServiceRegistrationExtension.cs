using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.FAA.CSJProxy.Api.AppStart;

[ExcludeFromCodeCoverage]
public static class AddServiceRegistrationExtension
{
    public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // validators
        services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
    }

    public static void ConfigureHealthChecks(this IServiceCollection services)
    {
        // health checks
        services
            .AddHealthChecks()
            .AddCheck<DefaultHealthCheck>("default");
    }
}