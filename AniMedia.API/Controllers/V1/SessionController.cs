using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Responses;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[Authorize]
public class SessionController : BaseApiV1Controller {

    private readonly ICurrentUserService _currentUserService;
    
    public SessionController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator) {
        _currentUserService = currentUserService;
    }

    [HttpGet("remove/{sessionUid:guid}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSession(Guid sessionUid, CancellationToken cancellationToken) {
        var query = new RemoveSessionCommand(_currentUserService.UserUID ?? Guid.Empty, sessionUid);

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(PagedResult<SessionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSessionList(int page, int pageSize, CancellationToken cancellationToken) {
        var query = new GetSessionListQueryCommand(_currentUserService.UserUID ?? Guid.Empty, page, pageSize);

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("{accessToken}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSession(string accessToken, CancellationToken cancellationToken) {
        var query = new GetSessionQueryCommand(_currentUserService.UserUID ?? Guid.Empty, accessToken);

        return await RequestAsync(query, cancellationToken);
    }
}