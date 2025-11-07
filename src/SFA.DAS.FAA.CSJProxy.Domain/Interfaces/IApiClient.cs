using SFA.DAS.FAA.CSJProxy.Domain.Models;

namespace SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
public interface IApiClient
{
    Task<ApiResponse<TResponse?>> GetWithResponseCodeAsync<TResponse>(IGetApiRequest request, CancellationToken cancellationToken = default);
}
