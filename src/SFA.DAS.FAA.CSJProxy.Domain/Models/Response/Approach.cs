using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record Approach
{
    [JsonProperty("en")]
    public string? En { get; set; }
}