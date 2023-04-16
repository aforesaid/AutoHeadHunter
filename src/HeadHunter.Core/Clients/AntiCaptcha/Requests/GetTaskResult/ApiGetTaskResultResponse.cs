using Newtonsoft.Json;

namespace HeadHunterManager.Core.AntiCaptcha.Requests.GetTaskResult
{
    public class ApiGetTaskResultResponse
    {
        [JsonProperty("errorId")] public long ErrorId { get; set; }

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Status { get; set; }

        [JsonProperty("solution", DefaultValueHandling = DefaultValueHandling.Include)]
        public ApiGetTaskResultResponseDetails Solution { get; set; }

        [JsonProperty("cost", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Cost { get; set; }

        [JsonProperty("ip", DefaultValueHandling = DefaultValueHandling.Include)]
        public string Ip { get; set; }

        [JsonProperty("createTime", DefaultValueHandling = DefaultValueHandling.Include)]
        public long CreateTime { get; set; }

        [JsonProperty("endTime", DefaultValueHandling = DefaultValueHandling.Include)]
        public long EndTime { get; set; }

        [JsonProperty("solveCount", DefaultValueHandling = DefaultValueHandling.Include)]
        public long SolveCount { get; set; }
    }

    public class ApiGetTaskResultResponseDetails
    {
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("url")] public Uri Url { get; set; }
    }
}