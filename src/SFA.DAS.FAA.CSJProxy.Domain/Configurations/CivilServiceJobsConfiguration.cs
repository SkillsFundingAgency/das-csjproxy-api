namespace SFA.DAS.FAA.CSJProxy.Domain.Configurations;
public record CivilServiceJobsConfiguration
{
    public required string ApiBaseUrl { get; init; }
    public required string ApiKey { get; init; }
}