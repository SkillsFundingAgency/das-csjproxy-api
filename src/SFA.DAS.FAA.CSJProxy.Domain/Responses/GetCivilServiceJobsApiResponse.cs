using Newtonsoft.Json;
using SFA.DAS.FAA.CSJProxy.Domain.Models.Response;

namespace SFA.DAS.FAA.CSJProxy.Domain.Responses;
public record GetCivilServiceJobsApiResponse
{
    [JsonProperty("meta")]
    public Meta MetaData { get; set; }

    [JsonProperty("jobs")] 
    public List<Job> Jobs { get; set; } = [];

    public class Meta
    {
        [JsonProperty("numberOfJobs")]
        public int NumberOfJobs { get; set; }
    }
}