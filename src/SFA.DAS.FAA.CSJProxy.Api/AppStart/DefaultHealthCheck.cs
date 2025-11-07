using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SFA.DAS.FAA.CSJProxy.Api.AppStart;

[ExcludeFromCodeCoverage]
public class DefaultHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // external endpoint health checks could go here
            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch
        {
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}