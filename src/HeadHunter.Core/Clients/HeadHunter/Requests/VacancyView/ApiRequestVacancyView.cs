using Newtonsoft.Json;

namespace HeadHunterManager.Core.HeadHunter.Requests.VacancyView;

public class ApiRequestVacancyView
{
    [JsonProperty("isBrandingPreview")]
    public bool IsBrandingPreview { get; set; }

    [JsonProperty("approved")]
    public bool Approved { get; set; }

    [JsonProperty("vacancyId")]
    public int VacancyId { get; set; }

    [JsonProperty("managerId")]
    public int ManagerId { get; set; }

    [JsonProperty("@vacancyCode")]
    public object VacancyCode { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("multi")]
    public bool Multi { get; set; }

    [JsonProperty("acceptTemporary")]
    public bool AcceptTemporary { get; set; }
    
    [JsonProperty("publicationDate")]
    public DateTime PublicationDate { get; set; }

    [JsonProperty("contactInfo")]
    public object ContactInfo { get; set; }

    [JsonProperty("validThroughTime")]
    public DateTime ValidThroughTime { get; set; }

    [JsonProperty("hr-brand")]
    public object HrBrand { get; set; }

    [JsonProperty("@workSchedule")]
    public string WorkSchedule { get; set; }

    [JsonProperty("@acceptHandicapped")]
    public bool AcceptHandicapped { get; set; }

    [JsonProperty("@acceptKids")]
    public bool AcceptKids { get; set; }

    [JsonProperty("insider")]
    public object Insider { get; set; }

    [JsonProperty("workExperience")]
    public string WorkExperience { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("platforms")]
    public IList<string> Platforms { get; set; }

    [JsonProperty("driverLicenseTypes")]
    public object DriverLicenseTypes { get; set; }

    [JsonProperty("mapDisabled")]
    public bool MapDisabled { get; set; }

    [JsonProperty("showSignupForm")]
    public bool ShowSignupForm { get; set; }

    [JsonProperty("showResumeForm")]
    public bool ShowResumeForm { get; set; }

    [JsonProperty("showSkillsSurvey")]
    public bool ShowSkillsSurvey { get; set; }

    [JsonProperty("skillsSurveyProfession")]
    public object SkillsSurveyProfession { get; set; }

    [JsonProperty("vacancyConstructorTemplate")]
    public object VacancyConstructorTemplate { get; set; }

    [JsonProperty("brandingType")]
    public object BrandingType { get; set; }

    [JsonProperty("systemInfo")]
    public object SystemInfo { get; set; }

    [JsonProperty("metallic")]
    public string Metallic { get; set; }

    [JsonProperty("metallicId")]
    public string MetallicId { get; set; }

    [JsonProperty("canViewResponses")]
    public bool CanViewResponses { get; set; }

    [JsonProperty("userTestId")]
    public object UserTestId { get; set; }

    [JsonProperty("userLabels")]
    public IList<object> UserLabels { get; set; }

    [JsonProperty("features")]
    public IList<string> Features { get; set; }

    [JsonProperty("vacancyOnMapLink")]
    public string VacancyOnMapLink { get; set; }

    [JsonProperty("publicationType")]
    public string PublicationType { get; set; }

    [JsonProperty("publicationTypeTrl")]
    public string PublicationTypeTrl { get; set; }
}