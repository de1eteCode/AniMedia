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
    public Task<IActionResult> GetProfile(CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    [HttpGet("update")]
    [ProducesResponseType(typeof(UpdateProfileResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> UpdateProfile(UpdateProfileRequest request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    [HttpGet("updateavatar")]
    [ProducesResponseType(typeof(UpdateProfileResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public Task<IActionResult> UpdateAvatar(IFormFile newAvatar, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}