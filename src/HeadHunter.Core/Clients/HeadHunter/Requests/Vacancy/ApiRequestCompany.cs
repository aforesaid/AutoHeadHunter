using Newtonsoft.Json;

namespace HeadHunterManager.Core.HeadHunter.Requests.Vacancy;

public class ApiRequestCompany
{
    [JsonProperty("@showSimilarVacancies")]
    public bool ShowSimilarVacancies { get; set; }

    [JsonProperty("@trusted")]
    public bool Trusted { get; set; }

    [JsonProperty("@countryId")]
    public long CountryId { get; set; }

    [JsonProperty("@structureName", NullValueHandling = NullValueHandling.Ignore)]
    public string StructureName { get; set; }

    [JsonProperty("@state")]
    public string State { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("visibleName")]
    public string VisibleName { get; set; }

    [JsonProperty("structureName", NullValueHandling = NullValueHandling.Ignore)]
    public string CompanyStructureName { get; set; }
    

    [JsonProperty("mainEmployerId", NullValueHandling = NullValueHandling.Ignore)]
    public long? MainEmployerId { get; set; }
    

    [JsonProperty("companySiteUrl")]
    public string CompanySiteUrl { get; set; }

    [JsonProperty("employerOrganizationFormId", NullValueHandling = NullValueHandling.Ignore)]
    public long? EmployerOrganizationFormId { get; set; }
}