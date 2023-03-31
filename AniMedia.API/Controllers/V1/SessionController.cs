using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[Authorize]
public class SessionController : BaseApiV1Controller {

    public SessionController(IMediator mediator) : base(mediator) {
    }

    [HttpGet("remove/{sessionUid:guid}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSession(Guid sessionUid, CancellationToken cancellationToken) {
        var query = new RemoveSessionCommand(sessionUid);

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(List<SessionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSessionList(CancellationToken cancellationToken) {
        var query = new GetSessionListQueryCommand();

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("{accessToken}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSession(string accessToken, CancellationToken cancellationToken) {
        var query = new GetSessionQueryCommand(accessToken);

        return await RequestAsync(query, cancellationToken);
    }
}