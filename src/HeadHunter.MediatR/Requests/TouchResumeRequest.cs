using MediatR;

namespace HeadHunter.MediatR.Requests;

public class TouchResumeRequest : IRequest<TouchResumeResponse>
{
    public TouchResumeRequest()
    { }

    public TouchResumeRequest(string resumeHash)
    {
        ResumeHash = resumeHash;
    }
    public string ResumeHash { get; set; }
}

public class TouchResumeResponse
{
    public TouchResumeResponse()
    { }

    public TouchResumeResponse(bool success)
    {
        Success = success;
    }
    public bool Success { get; set; }
}