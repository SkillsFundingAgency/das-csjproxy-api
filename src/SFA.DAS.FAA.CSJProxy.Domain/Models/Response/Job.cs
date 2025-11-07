using Newtonsoft.Json;

namespace SFA.DAS.FAA.CSJProxy.Domain.Models.Response;
public record Job
{
    [JsonProperty("country")]
    public Country Country { get; set; }

    [JsonProperty("jobAdvertOnly")]
    public bool JobAdvertOnly { get; set; }

    [JsonProperty("jobApplyURL")]
    public JobApplyUrl JobApplyUrl { get; set; }

    [JsonProperty("jobCode")]
    public string? JobCode { get; set; }

    [JsonProperty("jobReference")]
    public string? JobReference { get; set; }

    [JsonProperty("jobSystem")]
    public string? JobSystem { get; set; }

    [JsonProperty("jobTitle")]
    public JobTitle JobTitle { get; set; }

    [JsonProperty("jobURL")]
    public string? JobUrl { get; set; }

    [JsonProperty("keyTimes")]
    public KeyTimes KeyTimes { get; set; }

    [JsonProperty("approach")]
    public Approach Approach { get; set; }

    [JsonProperty("contractType")]
    public ContractType ContractType { get; set; }

    [JsonProperty("countryRegions")]
    public CountryRegions CountryRegions { get; set; }

    [JsonProperty("department")]
    public Department Department { get; set; }

    [JsonProperty("grade")]
    public Grade Grade { get; set; }

    [JsonProperty("profession")]
    public Profession Profession { get; set; }

    [JsonProperty("role")]
    public Role Role { get; set; }

    [JsonProperty("workingPattern")]
    public WorkingPattern WorkingPattern { get; set; }

    [JsonProperty("locationFlags")]
    public LocationFlags LocationFlags { get; set; }

    [JsonProperty("locationGeoCoordinates")]
    public List<LocationGeoCoordinate> LocationGeoCoordinates { get; set; } = [];

    [JsonProperty("salaryCurrency")]
    public string? SalaryCurrency { get; set; }

    [JsonProperty("salaryMaximum")]
    public decimal SalaryMaximum { get; set; }

    [JsonProperty("salaryMinimum")]
    public decimal SalaryMinimum { get; set; }
}
