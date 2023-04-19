using AniMedia.Application.ApiCommands.Account;
using AniMedia.Application.ApiQueries.Account;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[Authorize]
public class AccountController : BaseApiV1Controller {

    private readonly ICurrentUserService _currentUserService;
    
    public AccountController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator) {
        _currentUserService = currentUserService;
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(ProfileUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken) {
        var query = new GetProfileQueryCommand(_currentUserService.UserUID ?? Guid.Empty);

        return await RequestAsync(query, cancellationToken);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(ProfileUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateProfile(string firstName, string secondName, CancellationToken cancellationToken) {
        var request = new UpdateProfileCommand(_currentUserService.UserUID ?? Guid.Empty, firstName, secondName);

        return await RequestAsync(request, cancellationToken);
    }

    [HttpPost("updateavatar")]
    [ProducesResponseType(typeof(BinaryFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateAvatar(IFormFile newAvatar, CancellationToken cancellationToken) {
        var stream = newAvatar.OpenReadStream();

        if (stream == null) {
            // Todo
            throw new NotImplementedException();
        }

        var request = new UpdateAvatarCommand(_currentUserService.UserUID ?? Guid.Empty, stream, newAvatar.ContentType);

        return await RequestAsync(request, cancellationToken);
    }
}