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

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResponce>> Login(AuthorizationRequest request) {
        return Ok(await _authorizationService.Login(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponce>> Register(RegistrationRequest request) {
        return Ok(await _authorizationService.Register(request));
    }
}