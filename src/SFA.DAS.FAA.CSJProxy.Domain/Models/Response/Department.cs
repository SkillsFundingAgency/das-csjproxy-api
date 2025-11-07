using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record Department
{
    [JsonProperty("en")]
    public string? En { get; set; }
}