using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record KeyTimes
{
    [JsonProperty("closingTime")]
    public DateTime ClosingTime { get; set; }

    [JsonProperty("publishedTime")]
    public DateTime PublishedTime { get; set; }

    [JsonProperty("updatedTime")]
    public DateTime UpdatedTime { get; set; }
}