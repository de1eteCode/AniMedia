using AniMedia.Application.Exceptions;
using AniMedia.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthService = AniMedia.Identity.Contracts.IAuthorizationService;

namespace AniMedia.API.Controllers.V1;

[AllowAnonymous]
public class AccountController : BaseApiV1Controller {
    private readonly IAuthService _authorizationService;

    public AccountController(IAuthService authorizationService) {
        _authorizationService = authorizationService;
    }

    [HttpPost(nameof(Login), Name = nameof(Login))]
    [ProducesResponseType(typeof(AuthorizationResponce), 200)]
    [ProducesResponseType(typeof(BadRequestException), 400)]
    public async Task<ActionResult<AuthorizationResponce>> Login(AuthorizationRequest request) {
        return Ok(await _authorizationService.Login(request));
    }

    [HttpPost(nameof(Register), Name = nameof(Register))]
    [ProducesResponseType(typeof(RegistrationResponce), 200)]
    [ProducesResponseType(typeof(BadRequestException), 400)]
    [ProducesResponseType(typeof(IdentityException), 500)]
    public async Task<ActionResult<RegistrationResponce>> Register(RegistrationRequest request) {
        return Ok(await _authorizationService.Register(request));
    }
}