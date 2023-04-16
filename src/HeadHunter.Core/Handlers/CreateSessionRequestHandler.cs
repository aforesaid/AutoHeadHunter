using HeadHunter.Core.Clients.HeadHunter;
using HeadHunter.MediatR.Requests;
using MediatR;

namespace HeadHunter.Core.Handlers;

public class CreateSessionRequestHandler : IRequestHandler<CreateSessionRequest, CreateSessionResponse>
{
    private readonly IHeadHunterClient _headHunterClient;

    public CreateSessionRequestHandler(IHeadHunterClient headHunterClient)
    {
        _headHunterClient = headHunterClient;
    }

    public async Task<CreateSessionResponse> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
    {
        var xsrfToken = await _headHunterClient.CreateXsrfToken();
        var session = await _headHunterClient.Login(request.Username, request.Password, xsrfToken);

        return new CreateSessionResponse(xsrfToken: session.XsrfToken,
            hhUid: session.HhUid,
            hhToken: session.HhToken);
    }
}