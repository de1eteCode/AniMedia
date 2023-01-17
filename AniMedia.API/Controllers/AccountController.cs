using AniMedia.Application.Contracts.Identity;
using AniMedia.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase {
    private readonly IAuthorizationService _authorizationService;

    public AccountController(IAuthorizationService authorizationService) {
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