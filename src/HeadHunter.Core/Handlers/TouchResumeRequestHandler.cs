using HeadHunter.Core.Clients.HeadHunter;
using HeadHunter.Core.Configuration;
using HeadHunter.MediatR.Requests;
using MediatR;
using Microsoft.Extensions.Options;

namespace HeadHunter.Core.Handlers;

public class TouchResumeRequestHandler : IRequestHandler<TouchResumeRequest, TouchResumeResponse>
{
    private readonly HeadHunterSettings _headHunterSettings;
    private readonly IHeadHunterClient _headHunterClient;

    private readonly IMediator _mediator;

    public TouchResumeRequestHandler(IOptions<HeadHunterSettings> headHunterSettings, 
        IHeadHunterClient headHunterClient, 
        IMediator mediator)
    {
        _headHunterClient = headHunterClient;
        _mediator = mediator;
        _headHunterSettings = headHunterSettings.Value;
    }

    public async Task<TouchResumeResponse> Handle(TouchResumeRequest request, CancellationToken cancellationToken)
    {
        var target = _headHunterSettings.Users
            .SelectMany(x => x.ResumeConfigurations
                .Select(resume => new {User = x, Resume = resume}))
            .FirstOrDefault(x => x.Resume.ResumeHash == request.ResumeHash);

        if (target == null)
        {
            throw new ArgumentException($"Resume with resume hash {request.ResumeHash} not found");
        }

        var userInfo = target.User;
        var resume = target.Resume;

        var createSessionRequest = new CreateSessionRequest(userInfo.Username, userInfo.Password);
        var createSessionResponse = await _mediator.Send(createSessionRequest, cancellationToken);

        var sessionInfo = new HeadHunterSessionInfo(createSessionResponse.XsrfToken,
            createSessionResponse.HhUid,
            createSessionResponse.HhToken);

        var existResumes = await _headHunterClient.GetResumes(sessionInfo);

        var myTargetResume = existResumes.FirstOrDefault(x => x.Hash == resume.ResumeHash);
        if (myTargetResume == null)
        {
            throw new ArgumentException($"Resume with hash {resume.ResumeHash} not found on hh.ru");
        }

        var result = await _headHunterClient.TouchResume(resume.ResumeHash, sessionInfo);

        return new TouchResumeResponse(success: result);
    }
}