namespace HeadHunter.Core.Clients.AntiCaptcha
{
    public interface IAntiCaptchaApiClient
    {
        Task<long> CreateTask(string apiKey, string base64, string type, int maxLength = 0,
            int minLength = 0, int containsNumeric = 0);

        Task<string> GetTaskResult(string apiKey, long taskId);
    }
}