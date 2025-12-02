using SFA.DAS.FAA.CSJProxy.Domain.Interfaces;

namespace SFA.DAS.FAA.CSJProxy.Domain.Requests;
public record GetCivilServiceJobsApiRequest : IGetApiRequest
{
    public string GetUrl => "/csj/v1/jobs?contractType=Apprenticeship";
}