using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record JobApplyUrl
{
    [JsonProperty("en")]
    public string? En { get; set; }
}