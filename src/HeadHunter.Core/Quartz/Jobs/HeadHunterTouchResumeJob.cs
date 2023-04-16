using HeadHunter.Core.Configuration;
using HeadHunter.MediatR.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace HeadHunter.Core.Quartz.Jobs;

public class HeadHunterTouchResumeJob : IJob
{
    private readonly ILogger<HeadHunterTouchResumeJob> _logger;
    private readonly HeadHunterSettings _headHunterSettings;
    private readonly IMediator _mediator;

    public HeadHunterTouchResumeJob(IMediator mediator, 
        IOptions<HeadHunterSettings> headHunterSettings, 
        ILogger<HeadHunterTouchResumeJob> logger)
    {
        _mediator = mediator;
        _logger = logger;
        _headHunterSettings = headHunterSettings.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var resumes = _headHunterSettings.Users
                .SelectMany(x => x.ResumeConfigurations)
                .Select(x => x.ResumeHash);

            foreach (var resume in resumes)
            {
                try
                {
                    var touchResumeRequest = new TouchResumeRequest(resume);
                    await _mediator.Send(touchResumeRequest);
                    
                    _logger.LogInformation("Success touched resume {resumeHash}",
                        resumes);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while processing {processName} for resume {resumeHash}", 
                        nameof(HeadHunterTouchResumeJob),
                        resume);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while processing job {jobName}",
                nameof(HeadHunterTouchResumeJob));
        }
    }
}