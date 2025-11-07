using SFA.DAS.FAA.CSJProxy.Domain.Models;

namespace SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
public interface ICivilServiceApiService
{
    Task<bool> IsApiHealthyAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<TResponse?>> GetCivilServiceApiResponse<TResponse>(IGetApiRequest request, CancellationToken cancellationToken = default);
}