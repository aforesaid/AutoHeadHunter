using HeadHunter.Core.Clients.HeadHunter;
using HeadHunter.Core.Configuration;
using HeadHunter.MediatR.Requests;
using HeadHunterManager.Core.HeadHunter.Exceptions;
using HeadHunterManager.Core.HeadHunter.Requests.Vacancy;
using MediatR;
using Microsoft.Extensions.Options;

namespace HeadHunter.Core.Handlers;

public class RespondToSupportedVacanciesRequestHandler : IRequestHandler<RespondToSupportedVacanciesRequest, RespondToSupportedVacanciesResponse>
{
    private readonly HeadHunterSettings _headHunterSettings;
    private readonly IHeadHunterClient _headHunterClient;

    private readonly IMediator _mediator;

    public RespondToSupportedVacanciesRequestHandler(IOptions<HeadHunterSettings> headHunterSettings,
        IHeadHunterClient headHunterClient, 
        IMediator mediator)
    {
        _headHunterSettings = headHunterSettings.Value;
        _headHunterClient = headHunterClient;
        _mediator = mediator;
    }

    public async Task<RespondToSupportedVacanciesResponse> Handle(RespondToSupportedVacanciesRequest request, CancellationToken cancellationToken)
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
        
        var availableVacancies = new List<ApiRequestVacancy>();
    
        var page = 0;
        var touchResumeLimited = false;

        do
        {
            var vacancies = await _headHunterClient.GetVacanciesByQuery(resume.SearchQuery,
                sessionInfo,
                salary: resume.ExpectSalary,
                stopWords: resume.StopWords, 
                page: page);

            if (!vacancies.Any())
                break;

            availableVacancies.AddRange(vacancies);
            page++;
        } while (true);
        
        var supportedVacancies =
            availableVacancies.Where(x => x.VacancyId.HasValue
                                          && !x.UserLabels.Any()
                                          && x.ResponseLetterRequired != true
                                          && !x.UserTestId.HasValue)
                .ToList();
        
        var tasks = supportedVacancies.Select(async vacancy =>
        {
            if (touchResumeLimited)
                return;
            
            try
            {
                await _headHunterClient.VacancyRespond(resume.ResumeHash,
                    vacancy.VacancyId.Value,
                    resume.LetterRespondTemplate, sessionInfo);
            }
            catch(VacancyRespondLimitedException)
            {
                touchResumeLimited = true;
            }
        }).ToList();

        await Task.WhenAll(tasks);

        return new RespondToSupportedVacanciesResponse(success: true);
    }
}