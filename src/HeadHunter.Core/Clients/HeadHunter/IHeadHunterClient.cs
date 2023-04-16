using HeadHunter.Core.Configuration;
using HeadHunterManager.Core.HeadHunter.Requests.Resume;
using HeadHunterManager.Core.HeadHunter.Requests.Vacancy;
using HeadHunterManager.Core.HeadHunter.Requests.VacancyView;

namespace HeadHunter.Core.Clients.HeadHunter;

public interface IHeadHunterClient
{
    Task<string> CreateXsrfToken();
    Task<HeadHunterSessionInfo> Login(string username, string password, string xsrfToken);
    Task<bool> TouchResume(string resume, HeadHunterSessionInfo sessionInfo);
    Task<bool> VacancyRespond(string resume, long vacancyId, string letter, HeadHunterSessionInfo sessionInfo);
    Task<IEnumerable<ApiRequestVacancy>> GetVacanciesByQuery(string text,
        HeadHunterSessionInfo sessionInfo,
        string resume = null,
        long? salary = null,
        string[] stopWords = null,
        int page = 0,
        int itemOnPage = 100);

    Task<IEnumerable<ApiRequestResume>> GetResumes(HeadHunterSessionInfo sessionInfo);
    Task<ApiRequestVacancyView> GetVacancyView(long vacancyId, HeadHunterSessionInfo sessionInfo);
    /*Task<string> GetCaptchaKey(SessionInfo sessionInfo);
    Task<byte[]> GetCaptchaImage(string captchaKey, SessionInfo sessionInfo);*/
}