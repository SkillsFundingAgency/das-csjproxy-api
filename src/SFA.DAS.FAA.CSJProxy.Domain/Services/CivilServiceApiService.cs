using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Models;
using SFA.DAS.FAA.CSJProxy.Domain.Requests;
using SFA.DAS.FAA.CSJProxy.Domain.Responses;
using System.Net;

namespace SFA.DAS.FAA.CSJProxy.Domain.Services;
public class CivilServiceApiService(IApiClient apiClient) : ICivilServiceApiService
{
    public async Task<ApiResponse<TResponse?>> GetCivilServiceApiResponse<TResponse>(
        IGetApiRequest request,
        CancellationToken cancellationToken = default)
    {
        return await apiClient.GetWithResponseCodeAsync<TResponse>(request, cancellationToken);
    }

    public async Task<bool> IsApiHealthyAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await apiClient.GetWithResponseCodeAsync<GetCivilServiceJobsApiResponse>(
                new GetCivilServiceJobsApiRequest(),
                cancellationToken);

            return response?.StatusCode == HttpStatusCode.OK;
        }
        catch
        {
            return false;
        }
    }
}
