using AniMedia.Application.ApiCommands.Account;
using AniMedia.Application.ApiQueries.Account;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Requests;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[Authorize]
public class AccountController : BaseApiV1Controller {

    public AccountController(IMediator mediator) : base(mediator) {
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(ProfileUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken) {
        var query = new GetProfileQueryCommand();

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("update")]
    [ProducesResponseType(typeof(ProfileUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest profile, CancellationToken cancellationToken) {
        var request = new UpdateProfileCommand(profile.FirstName, profile.SecondName);

        return await RequestAsync(request, cancellationToken);
    }

    [HttpGet("updateavatar")]
    [ProducesResponseType(typeof(BinaryFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateAvatar(IFormFile newAvatar, CancellationToken cancellationToken) {
        var stream = newAvatar.OpenReadStream();

        if (stream == null) {
            // Todo
            throw new NotImplementedException();
        }

        var request = new UpdateAvatarCommand(stream, newAvatar.FileName, newAvatar.ContentType);

        return await RequestAsync(request, cancellationToken);
    }
}