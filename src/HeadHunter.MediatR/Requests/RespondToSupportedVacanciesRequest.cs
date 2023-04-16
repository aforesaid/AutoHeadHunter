using MediatR;

namespace HeadHunter.MediatR.Requests;

public class RespondToSupportedVacanciesRequest : IRequest<RespondToSupportedVacanciesResponse>
{
    public RespondToSupportedVacanciesRequest()
    { }

    public RespondToSupportedVacanciesRequest(string resumeHash)
    {
        ResumeHash = resumeHash;
    }
    public string ResumeHash { get; set; }
}

public class RespondToSupportedVacanciesResponse
{
    public RespondToSupportedVacanciesResponse()
    { }

    public RespondToSupportedVacanciesResponse(bool success)
    {
        Success = success;
    }
    public bool Success { get; set; }
}