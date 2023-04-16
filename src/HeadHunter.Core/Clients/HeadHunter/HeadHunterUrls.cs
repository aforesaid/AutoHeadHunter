using HeadHunter.Core.Configuration;

namespace HeadHunter.Core.Clients.HeadHunter;

public static class HeadHunterUrls
{
    public static void SetCredentials(this HttpRequestMessage message, 
        HeadHunterSessionInfo sessionInfo)
    {
        message.Headers.Add("origin", "hh.ru");
        message.Headers.Add("referer", "https://hh.ru");
        
        message.Headers.Add("Cookie", sessionInfo.Cookie);
        message.Headers.Add("x-xsrftoken", sessionInfo.XsrfToken);
    }

    public static HttpRequestMessage CreateSession()
    {
        var uri = new Uri("https://hh.ru/account/login");

        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        return message;
    }
    
    public static HttpRequestMessage Login(string username,
        string password,
        string xsrfToken)
    {
        var uri = new Uri("https://hh.ru/account/login?backurl=%2F");
        var content = new MultipartFormDataContent("------WebKitFormBoundaryOlbLIZU1OcgxkLPm");
        
        content.Add(new StringContent(xsrfToken), "_xsrf");
        content.Add(new StringContent(username), "username");
        content.Add(new StringContent(password), "password");
        
        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = content
        };
        
        message.Headers.Add("accept","application/json");
        
        return message;
    }
    
    public static HttpRequestMessage PostTouchResume(string resume, 
        HeadHunterSessionInfo sessionInfo,
        bool undirectable = true)
    {
        var uri = new Uri("https://hh.ru/applicant/resumes/touch");
        var content = new MultipartFormDataContent("------WebKitFormBoundaryOlbLIZU1OcgxkLPm");
        content.Add(new StringContent(resume), "resume");
        content.Add(new StringContent(undirectable.ToString()), "undirectable");
        
        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = content
        };
        
        message.Headers.Add("accept", "application/json");
    
        
        message.SetCredentials(sessionInfo);
        
        return message;
    }

    public static HttpRequestMessage PostVacancyRespond(string resume, 
        string letter,
        long vacancyId, 
        HeadHunterSessionInfo sessionInfo,
        bool letterRequired = false, 
        bool lux = true, 
        bool ignorePostponed = true)
    {
        var uri = new Uri("https://hh.ru/applicant/vacancy_response/popup");
        
        var content = new MultipartFormDataContent("----WebKitFormBoundaryOlbLIZU1OcgxkLPm");
        content.Add(new StringContent(resume), "resume_hash");
        content.Add(new StringContent(vacancyId.ToString()), "vacancy_id");    
        content.Add(new StringContent(letterRequired.ToString()), "letterRequired");
        content.Add(new StringContent(lux.ToString()),  "lux");
        content.Add(new StringContent(letter), "letter");
        content.Add(new StringContent(ignorePostponed.ToString()), "ignore_postponed");

        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = content
        };
        
        
        message.SetCredentials(sessionInfo);

        return message;
    }

    public static HttpRequestMessage GetVacanciesByQuery(string text,
        HeadHunterSessionInfo sessionInfo,
        string resume = null,
        long? salary = null, 
        string[] stopWords = null,
        int page = 0,
        int itemsOnPage = 100)
    {
        const string addressUri = "https://hh.ru/search/vacancy?";
       
        var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
       
        queryString.Add("items_on_page", itemsOnPage.ToString());
        queryString.Add("text", text);
        queryString.Add("employment", "full");

        if (!string.IsNullOrWhiteSpace(resume))
        {
            queryString.Add("resume", resume);
        }

        if (salary.HasValue)
        {
            queryString.Add("salary", salary.ToString());
        }

        if (page != default)
        {
            queryString.Add("page", page.ToString());
        }

        if (stopWords != null)
        {
            var excludeText = string.Join(',', stopWords);
            queryString.Add("excluded_text", excludeText);
        }

        var uri = new Uri(addressUri + queryString);
        
        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        
        message.Headers.Add("accept", "text/html");
        
        message.SetCredentials(sessionInfo);
        return message;
    }
    
    public static HttpRequestMessage GetResumes(HeadHunterSessionInfo sessionInfo)
    {
        var uri = new Uri("https://hh.ru/applicant/resumes");

        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        
        message.Headers.Add("accept", "text/html");
        
        message.SetCredentials(sessionInfo);
        return message;
    }

    public static HttpRequestMessage GetVacancyView(long vacancyId,
        HeadHunterSessionInfo sessionInfo)
    {
        var uri = new Uri($"https://hh.ru/vacancy/{vacancyId}");
        
        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        
        message.Headers.Add("accept", "text/html");
        
        message.SetCredentials(sessionInfo);
        return message;
    }

    /*public static HttpRequestMessage PostCaptchaKey(string cookie, string xsrfToken)
    {
        var uri = new Uri("https://hh.ru/captcha?lang=RU");

        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri
        };
        
        message.Headers.Add("accept", "application/json");
        
        message.SetCredentials(cookie, xsrfToken);
        return message;
    }
    
    public static HttpRequestMessage GetCaptchaImage(string captchaKey,
        string cookie,
        string xsrfToken)
    {
        const string addressUri = "https://hh.ru/captcha/picture?";
       
        var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
        queryString.Add("key", captchaKey);
        
        var uri = new Uri(addressUri + queryString);

        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = uri
        };
        
        message.Headers.Add("accept", "image/webp,image/apng,image/svg+xml,image/*,#1#*;q=0.8");
        
        message.SetCredentials(cookie, xsrfToken);
        return message;
    }


    public static HttpRequestMessage PostSolveCaptcha(string captchaKey, string captchaState,
        string captchaText,
        string cookie, 
        string xsrfToken)
    {
        const string addressUri = "https://hh.ru/account/captcha?";
        var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        queryString.Add("state", captchaState);
        queryString.Add("captchaKey", captchaKey);
        queryString.Add("captchaText", captchaText);
        
        var uri = new Uri(addressUri + queryString);
        
        var message = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = null
        };
        
        message.Headers.Add("accept", "application/json");
        
        message.SetCredentials(cookie, xsrfToken);
        return message;
    }*/
}