using MediatR;

namespace HeadHunter.MediatR.Requests;

public class CreateSessionRequest : IRequest<CreateSessionResponse>
{
    public CreateSessionRequest()
    { }

    public CreateSessionRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class CreateSessionResponse
{
    public CreateSessionResponse()
    { }
    
    public CreateSessionResponse(string xsrfToken, string hhUid, string hhToken)
    {
        XsrfToken = xsrfToken;
        HhUid = hhUid;
        HhToken = hhToken;
    }
    public string XsrfToken { get; set; }
    public string HhUid { get; set; }
    public string HhToken { get; set; }
}