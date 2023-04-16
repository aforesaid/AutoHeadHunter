using HeadHunter.Core.Configuration;
using HeadHunter.MediatR.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace HeadHunter.Core.Quartz.Jobs;

public class HeadHunterRespondToSupportedVacanciesJob : IJob
{
    private readonly ILogger<HeadHunterRespondToSupportedVacanciesJob> _logger;
    private readonly HeadHunterSettings _headHunterSettings;
    private readonly IMediator _mediator;

    public HeadHunterRespondToSupportedVacanciesJob(ILogger<HeadHunterRespondToSupportedVacanciesJob> logger, 
        IOptions<HeadHunterSettings> headHunterSettings, 
        IMediator mediator)
    {
        _logger = logger;
        _headHunterSettings = headHunterSettings.Value;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var resumes = _headHunterSettings.Users
                .SelectMany(x => x.ResumeConfigurations)
                .Where(x => x.AutoApply)
                .Select(x => x.ResumeHash);

            foreach (var resume in resumes)
            {
                try
                {
                    var respondToSupportedVacanciesRequest = new RespondToSupportedVacanciesRequest(resume);
                    await _mediator.Send(respondToSupportedVacanciesRequest);
                    
                    _logger.LogInformation("Success responded to vacancies for resume {resumeHash}",
                        resume);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while processing {processName} for resume {resumeHash}", 
                        nameof(HeadHunterRespondToSupportedVacanciesJob),
                        resume);
                }
            }    
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while processing job {jobName}",
                nameof(HeadHunterRespondToSupportedVacanciesJob));
        }
    }
}