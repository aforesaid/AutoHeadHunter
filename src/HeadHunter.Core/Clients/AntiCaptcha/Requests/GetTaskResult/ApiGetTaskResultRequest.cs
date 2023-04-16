using Newtonsoft.Json;

namespace HeadHunterManager.Core.AntiCaptcha.Requests.GetTaskResult
{
    public class ApiGetTaskResultRequest
    {
        public ApiGetTaskResultRequest()
        {
        }

        public ApiGetTaskResultRequest(string clientKey, long taskId)
        {
            ClientKey = clientKey;
            TaskId = taskId;
        }

        [JsonProperty("clientKey")] public string ClientKey { get; set; }

        [JsonProperty("taskId")] public long TaskId { get; set; }
    }
}