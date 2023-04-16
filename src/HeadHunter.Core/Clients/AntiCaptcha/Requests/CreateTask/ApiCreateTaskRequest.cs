using Newtonsoft.Json;

namespace HeadHunterManager.Core.AntiCaptcha.Requests.CreateTask
{
    public class ApiCreateTaskRequest
    {
        public ApiCreateTaskRequest()
        { }

        public ApiCreateTaskRequest(string clientKey, ApiCreateTaskRequestDetails task, string languagePool = "ru")
        {
            ClientKey = clientKey;
            LanguagePool = languagePool;
            Task = task;
        }

        [JsonProperty("clientKey")] public string ClientKey { get; set; }

        [JsonProperty("languagePool")] public string LanguagePool { get; set; }

        [JsonProperty("task")] public ApiCreateTaskRequestDetails Task { get; set; }
    }

    public class ApiCreateTaskRequestDetails
    {
        public ApiCreateTaskRequestDetails()
        {
        }

        public ApiCreateTaskRequestDetails(string type, string body, long minLength, long maxLength, long numeric)
        {
            Type = type;
            Body = body;
            MinLength = minLength;
            MaxLength = maxLength;
            Numeric = numeric;
        }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("body")] public string Body { get; set; }

        [JsonProperty("minLength")] public long MinLength { get; set; }

        [JsonProperty("maxLength")] public long MaxLength { get; set; }

        [JsonProperty("numeric")] public long Numeric { get; set; }
    }
}