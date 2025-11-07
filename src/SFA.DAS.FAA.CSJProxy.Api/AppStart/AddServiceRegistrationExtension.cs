using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Polly;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
using SFA.DAS.FAA.CSJProxy.Domain.Configurations;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Services;
using SFA.DAS.FAA.CSJProxy.Infrastructure.Api;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.FAA.CSJProxy.Api.AppStart;

[ExcludeFromCodeCoverage]
public static class AddServiceRegistrationExtension
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        // validators
        services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
        
        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCivilServiceJobsQuery).Assembly));

        // configuration options
        services.AddScoped<ICivilServiceApiService, CivilServiceApiService>();
        services.AddHttpClient<IApiClient, ApiClient>().ConfigureHttpClient((serviceProvider, client) =>
        {
            var civilServiceJobsConfiguration = serviceProvider.GetService<IOptions<CivilServiceJobsConfiguration>>()!.Value;
            client.BaseAddress = new Uri(civilServiceJobsConfiguration.Url);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("x-api-key", civilServiceJobsConfiguration.ApiKey);
        }).AddPolicyHandler(GetRetryPolicy());
    }

    public static void ConfigureHealthChecks(this IServiceCollection services)
    {
        // health checks
        services
            .AddHealthChecks()
            .AddCheck<DefaultHealthCheck>("default")
            .AddCheck<CivilServiceApiHealthCheck>("Civil Service Jobs Api Health Check",
                failureStatus: HealthStatus.Degraded, tags: ["ready"]);
    }

    public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<AzureActiveDirectoryConfiguration>(configuration.GetSection("AzureAd"));
        services.AddSingleton(cfg => cfg.GetService<IOptions<AzureActiveDirectoryConfiguration>>()!.Value);
        services.Configure<CivilServiceJobsConfiguration>(configuration.GetSection(nameof(CivilServiceJobsConfiguration)));
        services.AddSingleton(cfg => cfg.GetService<IOptions<CivilServiceJobsConfiguration>>()!.Value);
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}