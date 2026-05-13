using MediatR;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;
using SFA.DAS.FAA.CSJProxy.Domain.Responses;

namespace SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
public class GetCivilServiceJobsQueryHandler(
    ICivilServiceApiService apiService) : IRequestHandler<GetCivilServiceJobsQuery, GetCivilServiceJobsQueryResult>
{
    public async Task<GetCivilServiceJobsQueryResult> Handle(GetCivilServiceJobsQuery query, CancellationToken cancellationToken)
    {
        var response = await apiService.GetCivilServiceApiResponse<GetCivilServiceJobsApiResponse>(new GetCivilServiceJobsApiRequest(), cancellationToken);
   
        if (response.StatusCode != System.Net.HttpStatusCode.OK 
            || response.Body is null 
            || response.Body.Jobs.Count == 0)
        {
            return new GetCivilServiceJobsQueryResult();
        }

        return new GetCivilServiceJobsQueryResult
        {
            Jobs = response.Body.Jobs
                .Where(job => job.Country.En != null && job.Approach.En != null
                              && job.Country.En.Contains("England", StringComparison.CurrentCultureIgnoreCase)
                              && !job.Approach.En.Contains("Internal", StringComparison.CurrentCultureIgnoreCase)
                       )
                .ToList()
        };
    }
}