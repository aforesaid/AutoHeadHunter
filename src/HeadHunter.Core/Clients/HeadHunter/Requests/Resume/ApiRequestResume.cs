using Newtonsoft.Json;

namespace HeadHunterManager.Core.HeadHunter.Requests.Resume;

public class ApiRequestResume
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("hhid")]
    public string Hhid { get; set; }

    [JsonProperty("permission")]
    public string Permission { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("lastEditTime")]
    public long LastEditTime { get; set; }

    [JsonProperty("lang")]
    public string Lang { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }

    [JsonProperty("markServiceExpireTime")]
    public string MarkServiceExpireTime { get; set; }

    [JsonProperty("renewalServiceExpireTime")]
    public string RenewalServiceExpireTime { get; set; }

    [JsonProperty("publishState")]
    public object PublishState { get; set; }

    [JsonProperty("nextPublishAt")]
    public object NextPublishAt { get; set; }

    [JsonProperty("canPublishOrUpdate")]
    public bool CanPublishOrUpdate { get; set; }

    [JsonProperty("hasConditions")]
    public bool HasConditions { get; set; }

    [JsonProperty("percent")]
    public long Percent { get; set; }

    [JsonProperty("isSearchable")]
    public bool IsSearchable { get; set; }

    [JsonProperty("hasErrors")]
    public bool HasErrors { get; set; }

    [JsonProperty("hasPublicVisibility")]
    public bool HasPublicVisibility { get; set; }

    [JsonProperty("sitePlatform")]
    public string SitePlatform { get; set; }

    public string User { get; set; }

    [JsonProperty("updated")]
    public long Updated { get; set; }

    [JsonProperty("vacancySearchLastUsageDate")]
    public long VacancySearchLastUsageDate { get; set; }

    [JsonProperty("created")]
    public long Created { get; set; }

    [JsonProperty("marked")]
    public bool Marked { get; set; }

    [JsonProperty("renewal")]
    public bool Renewal { get; set; }

    [JsonProperty("moderated")]
    public DateTimeOffset Moderated { get; set; }

    [JsonProperty("validation_schema")]
    public string ValidationSchema { get; set; }

    [JsonProperty("update_timeout")]
    public long UpdateTimeout { get; set; }
}