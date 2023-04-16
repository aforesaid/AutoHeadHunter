namespace HeadHunter.Core.Configuration;

public class HeadHunterSettings
{
    public HeadHunterSettings()
    { }

    public HeadHunterSettings(IEnumerable<HeadHunterUserConfiguration> users)
    {
        Users = users;
    }
    public IEnumerable<HeadHunterUserConfiguration> Users { get; set; }
}

public class HeadHunterUserConfiguration
{
    public HeadHunterUserConfiguration()
    { }

    public HeadHunterUserConfiguration(string username, 
        string password, 
        IEnumerable<HeadHunterResumeConfiguration> resumeConfigurations)
    {
        Username = username;
        Password = password;
        ResumeConfigurations = resumeConfigurations;
    }
    public string Username { get; set; }
    public string Password { get; set; }
    public IEnumerable<HeadHunterResumeConfiguration> ResumeConfigurations { get; set; }
}

public class HeadHunterResumeConfiguration
{
    public HeadHunterResumeConfiguration()
    { }

    public HeadHunterResumeConfiguration(string resumeHash, 
        string searchQuery,
        string letterRespondTemplate,
        bool autoTouch, string[] stopWords,
        int expectSalary)
    {
        ResumeHash = resumeHash;
        SearchQuery = searchQuery;
        LetterRespondTemplate = letterRespondTemplate;
        AutoTouch = autoTouch;
        StopWords = stopWords;
        ExpectSalary = expectSalary;
    }
    public string ResumeHash { get; set; }
    public string SearchQuery { get; set; }
    public string LetterRespondTemplate { get; set; }

    public bool AutoTouch { get; set; }
    public bool AutoApply { get; set; }
    public string[] StopWords { get; set; }
    public int ExpectSalary { get; set; }
}