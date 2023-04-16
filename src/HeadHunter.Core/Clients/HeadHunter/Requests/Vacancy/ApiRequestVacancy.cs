using Newtonsoft.Json;

namespace HeadHunterManager.Core.HeadHunter.Requests.Vacancy;

public class ApiRequestVacancy
{
     [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("cId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? CId { get; set; }
        
        

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("href", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Href { get; set; }

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }

        [JsonProperty("rate", NullValueHandling = NullValueHandling.Ignore)]
        public double? Rate { get; set; }

        [JsonProperty("defaultCurrency", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DefaultCurrency { get; set; }

        [JsonProperty("site", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Site { get; set; }

        [JsonProperty("@responseLetterRequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ResponseLetterRequired { get; set; }

        [JsonProperty("@showContact", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowContact { get; set; }

        [JsonProperty("vacancyId", NullValueHandling = NullValueHandling.Ignore)]
        public long? VacancyId { get; set; }

        [JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
        public ApiRequestCompany Company { get; set; }
        
        [JsonProperty("snippet", NullValueHandling = NullValueHandling.Ignore)]
        public ApiRequestSnippet Snippet { get; set; }
        
        [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        public string Template { get; set; }

        [JsonProperty("creationSite", NullValueHandling = NullValueHandling.Ignore)]
        public string CreationSite { get; set; }

        [JsonProperty("creationSiteId", NullValueHandling = NullValueHandling.Ignore)]
        public long? CreationSiteId { get; set; }

        [JsonProperty("creationTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreationTime { get; set; }

        [JsonProperty("canBeShared", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CanBeShared { get; set; }
        

        [JsonProperty("inboxPossibility", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InboxPossibility { get; set; }

        [JsonProperty("notify", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Notify { get; set; }
        

        [JsonProperty("acceptIncompleteResumes", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AcceptIncompleteResumes { get; set; }
        
        [JsonProperty("userLabels", NullValueHandling = NullValueHandling.Ignore)]
        public string[] UserLabels { get; set; }
        

        [JsonProperty("responsesCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? ResponsesCount { get; set; }

        [JsonProperty("online_users_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? OnlineUsersCount { get; set; }
        
        [JsonProperty("userTestId", NullValueHandling = NullValueHandling.Ignore)]
        public long? UserTestId { get; set; }
        
        [JsonProperty("string", NullValueHandling = NullValueHandling.Ignore)]
        public string String { get; set; }
}