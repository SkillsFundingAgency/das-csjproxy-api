using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record LocationFlags
{
    [JsonProperty("countryRegions")]
    public bool CountryRegions { get; set; }

    [JsonProperty("locationGeoCoordinates")]
    public bool LocationGeoCoordinates { get; set; }

    [JsonProperty("overseas")]
    public bool Overseas { get; set; }

    [JsonProperty("remoteWorking")]
    public bool RemoteWorking { get; set; }
}