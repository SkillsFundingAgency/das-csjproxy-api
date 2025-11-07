using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;
using SFA.DAS.FAA.CSJProxy.Domain.Responses;

namespace SFA.DAS.FAA.CSJProxy.Application.Queries.CivilServiceJobs;
public class GetCivilServiceJobsQueryHandler(
    IApiClient apiClient,
    ILogger<GetCivilServiceJobsQueryHandler> logger) : IRequestHandler<GetCivilServiceJobsQuery, GetCivilServiceJobsQueryResult>
{
    public async Task<GetCivilServiceJobsQueryResult> Handle(GetCivilServiceJobsQuery request, CancellationToken cancellationToken)
    {
        // Fetch the civil service jobs from the API
        logger.LogInformation("Fetching Civil Service Jobs from API");

        // This is temp logger to check the outbound IP address for debugging purposes.
        var outboundIp = await LogOutboundIpAsync();
        logger.LogInformation("Outbound IP for CSJ API request: {ip}", outboundIp);

        var response = await apiClient.GetWithResponseCodeAsync<GetCivilServiceJobsApiResponse>(new GetCivilServiceJobsApiRequest(), cancellationToken);

        // Log the response details. Temp logging for debugging purposes.
        logger.LogInformation("CSJ Response code from API: {response}", response.StatusCode);
        logger.LogInformation("CSJ Response Error from API: {response}", response.ErrorContent);
        logger.LogInformation("CSJ Response from API: {response}", response.Body);
        
        if (response.StatusCode != System.Net.HttpStatusCode.OK 
            || response.Body is null 
            || response.Body.Jobs.Count == 0)
        {
            return new GetCivilServiceJobsQueryResult();
        }

        return new GetCivilServiceJobsQueryResult
        {
            Jobs = response.Body.Jobs
        };
    }

    private static async Task<string> LogOutboundIpAsync()
    {
        using var client = new HttpClient();
        var ip = await client.GetStringAsync("https://api.ipify.org");
        return ip;
    }
}