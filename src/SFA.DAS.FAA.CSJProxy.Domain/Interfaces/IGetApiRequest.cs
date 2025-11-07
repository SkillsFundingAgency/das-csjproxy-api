using System.Text.Json.Serialization;

namespace SFA.DAS.FAA.CSJProxy.Domain.Interfaces;
public interface IGetApiRequest
{
    [JsonIgnore]
    string GetUrl { get; }
}