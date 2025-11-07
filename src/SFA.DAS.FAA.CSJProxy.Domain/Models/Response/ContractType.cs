using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record ContractType
{
    [JsonProperty("en")]
    public List<string> En { get; set; } = [];
}