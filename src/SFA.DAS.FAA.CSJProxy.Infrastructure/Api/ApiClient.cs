using Newtonsoft.Json;
using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
using SFA.DAS.FAA.CSJProxy.Domain.Models;

namespace SFA.DAS.FAA.CSJProxy.Infrastructure.Api;
public class ApiClient(HttpClient httpClient) : IApiClient
{
    public async Task<ApiResponse<TResponse?>> GetWithResponseCodeAsync<TResponse>(IGetApiRequest request, CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, request.GetUrl);
        var response = await httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode
            ? new ApiResponse<TResponse?>(JsonConvert.DeserializeObject<TResponse>(responseContent), response.StatusCode, null)
            : new ApiResponse<TResponse?>(default, response.StatusCode, responseContent);
    }
}