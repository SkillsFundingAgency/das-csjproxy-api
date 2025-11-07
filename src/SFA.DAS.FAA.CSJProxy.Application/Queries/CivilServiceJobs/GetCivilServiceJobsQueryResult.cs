using SFA.DAS.FAA.CSJProxy.Domain.Models.Response;

namespace SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;

public record GetCivilServiceJobsQueryResult
{
    public List<Job> Jobs { get; init; } = [];
}