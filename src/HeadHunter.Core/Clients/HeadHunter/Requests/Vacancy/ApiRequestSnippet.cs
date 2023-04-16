using Newtonsoft.Json;

namespace HeadHunterManager.Core.HeadHunter.Requests.Vacancy;

public class ApiRequestSnippet
{
    [JsonProperty("req")]
    public string Req { get; set; }

    [JsonProperty("resp")]
    public string Resp { get; set; }

    [JsonProperty("cond")]
    public string Cond { get; set; }

    [JsonProperty("skill")]
    public string Skill { get; set; }

    [JsonProperty("desc")]
    public object Desc { get; set; }
}