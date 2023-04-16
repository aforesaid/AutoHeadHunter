using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using AngleSharp.Html.Parser;
using HeadHunter.Core.Configuration;
using HeadHunterManager.Core.HeadHunter.Exceptions;
using HeadHunterManager.Core.HeadHunter.Requests.Resume;
using HeadHunterManager.Core.HeadHunter.Requests.Vacancy;
using HeadHunterManager.Core.HeadHunter.Requests.VacancyView;
using Newtonsoft.Json.Linq;

namespace HeadHunter.Core.Clients.HeadHunter;

public class HeadHunterClient : IHeadHunterClient, IDisposable
{
    private readonly HttpClient _httpClient;

    public HeadHunterClient()
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.AllowAutoRedirect = false;
        
        _httpClient = new HttpClient(httpClientHandler);
    }

    public async Task<string> CreateXsrfToken()
    {
        var message = HeadHunterUrls.CreateSession();
        var response = await _httpClient.SendAsync(message);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new UnsupportedException();
        }

        const string xsrfField = "_xsrf";
        var xsrfToken = GetCookieValue(response.Headers, xsrfField);
        return xsrfToken;
    }
    
    public async Task<HeadHunterSessionInfo> Login(string username, string password, string xsrfToken)
    {
        var message = HeadHunterUrls.Login(username, password,
            xsrfToken);
        
        var response = await _httpClient.SendAsync(message);
        
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new UnsupportedException();
        }
        
        const string hhUidField = "hhuid";
        const string hhTokenField = "hhtoken";

        var hhUid = GetCookieValue(response.Headers, hhUidField);
        var hhToken = GetCookieValue(response.Headers, hhTokenField);

        if (string.IsNullOrWhiteSpace(hhToken))
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var error = JObject.Parse(responseContent)[""]?
                .ToObject<string>();
            
            throw new ExpectRecaptchaException();
        }
        
        var sessionInfo = new HeadHunterSessionInfo(xsrfToken,
            hhUid,
            hhToken);
        
        return sessionInfo;
    }

    public async Task<bool> TouchResume(string resume, HeadHunterSessionInfo sessionInfo)
    {
        var message = HeadHunterUrls.PostTouchResume(resume,
            sessionInfo);
        var response = await _httpClient.SendAsync(message);
        
        if (response.StatusCode == HttpStatusCode.Conflict)
            throw new ResumeAlreadyTouchedException();

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> VacancyRespond(string resume, long vacancyId, string letter, HeadHunterSessionInfo sessionInfo)
    {
        var message = HeadHunterUrls.PostVacancyRespond(resume, 
            letter, 
            vacancyId,
            sessionInfo);
        
        var response = await _httpClient.SendAsync(message);
        var responseContent = await response.Content.ReadAsStringAsync();

        var error = JObject.Parse(responseContent)["error"]?
            .ToObject<string>();

        const string limited = "negotiations-limit-exceeded";
        const string testRequired = "test-required";
        
        if (error == limited)
            throw new VacancyRespondLimitedException();

        if (error == testRequired)
            throw new TestRequiredException();

        return response.IsSuccessStatusCode;
    }


    public async Task<IEnumerable<ApiRequestVacancy>> GetVacanciesByQuery(string text, 
        HeadHunterSessionInfo sessionInfo,
        string resume = null,
        long? salary = null, 
        string[] stopWords = null,
        int page = 0,
        int itemsOnPage = 100)
    {
        var message = HeadHunterUrls.GetVacanciesByQuery(text, sessionInfo,
            resume: resume, 
            salary: salary,
            stopWords, page,
            itemsOnPage);
        var response = await _httpClient.SendAsync(message);

        var responseString = await response.Content.ReadAsStringAsync();
        var jData = GetJsonDataFromHtml(responseString);
        
        var items = jData?["vacancySearchResult"]?["vacancies"]?.ToObject<IEnumerable<ApiRequestVacancy>>();
        return items;
    }

    public async Task<IEnumerable<ApiRequestResume>> GetResumes(HeadHunterSessionInfo sessionInfo)
    {
        var message = HeadHunterUrls.GetResumes(sessionInfo);
        
        var response = await _httpClient.SendAsync(message);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new UnsupportedException();
        }
        var jData = GetJsonDataFromHtml(responseContent);
        
        var items = jData?["applicantResumes"]?.Select(x => x["_attributes"]?.ToObject<ApiRequestResume>());
        return items;
    }

    public async Task<ApiRequestVacancyView> GetVacancyView(long vacancyId, HeadHunterSessionInfo sessionInfo)
    {
        var message = HeadHunterUrls.GetVacancyView(vacancyId,
            sessionInfo);
        
        var response = await _httpClient.SendAsync(message);
        if (response.StatusCode == HttpStatusCode.Redirect)
        {
            throw new UnsupportedException();
        }
        
        var responseString = await response.Content.ReadAsStringAsync();

        var jData = GetJsonDataFromHtml(responseString);
        
        var vacancyView = jData?["vacancyView"]?.ToObject<ApiRequestVacancyView>();
        return vacancyView;
    }

    /*public async Task<string> GetCaptchaKey(SessionInfo sessionInfo)
    {
        var message = HeadHunterUrls.PostCaptchaKey(sessionInfo.CookieAsString,
            sessionInfo.XsrfToken);
        var response = await _httpClient.SendAsync(message);
        
        var result = await response.Content.ReadAsStringAsync();

        var jObject = JObject.Parse(result);

        var captchaKey = jObject["key"]?.ToObject<string>();
        return captchaKey;
    }

    public async Task<byte[]> GetCaptchaImage(string captchaKey, SessionInfo sessionInfo)
    {
        var message = HeadHunterUrls.GetCaptchaImage(captchaKey,
            sessionInfo.CookieAsString,
            sessionInfo.XsrfToken);
        var response = await _httpClient.SendAsync(message);
        
        var result = await response.Content.ReadAsByteArrayAsync();
        return result;
    }*/

    private JObject GetJsonDataFromHtml(string content)
    {
        var parser = new HtmlParser();
        
        var htmlContent = parser.ParseDocument(content);
        const string idElement = "HH-Lux-InitialState";
        
        var element = htmlContent.GetElementById(idElement);
        if (element == null)
            return null;

        var data = Regex.Replace(element.OuterHtml, "<.*?>", string.Empty);

        return JObject.Parse(data);
    }

    private static string GetCookieValue(HttpResponseHeaders headers, string parameter)
    {
        const string cookieField = "Set-Cookie";
        var cookies = headers.Where(x => x.Key == cookieField)
            .Select(x => x.Value)
            .ToList();

        var xsrfTokenCookie = cookies
            .SelectMany(x => x)
            .FirstOrDefault(x => x.Contains(parameter));

        if (xsrfTokenCookie == null)
            return null;
        
        var pattern = $@"(.*?){parameter}=(.*?);";
        var expression = Regex.Match(xsrfTokenCookie, pattern);
        var xsrfTokenValue = expression.Groups[2].Value;

        return xsrfTokenValue;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}