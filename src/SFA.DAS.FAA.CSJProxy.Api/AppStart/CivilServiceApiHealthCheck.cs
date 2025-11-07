using Microsoft.Extensions.Diagnostics.HealthChecks;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.FAA.CSJProxy.Api.AppStart;

[ExcludeFromCodeCoverage]
public class CivilServiceApiHealthCheck(ICivilServiceApiService service) : IHealthCheck
{
    private const string HealthCheckResultDescription = "Civil Service Jobs Api connection";

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await service.IsApiHealthyAsync(cancellationToken);

        return response
            ? HealthCheckResult.Healthy(HealthCheckResultDescription) 
            : HealthCheckResult.Degraded(HealthCheckResultDescription);
    }
}