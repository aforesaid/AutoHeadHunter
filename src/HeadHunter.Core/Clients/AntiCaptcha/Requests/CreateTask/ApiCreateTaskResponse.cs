using Newtonsoft.Json;

namespace HeadHunterManager.Core.AntiCaptcha.Requests.CreateTask
{
    public class ApiCreateTaskResponse
    {
        [JsonProperty("errorId")] public long ErrorId { get; set; }

        [JsonProperty("taskId")] public long TaskId { get; set; }
    }
}