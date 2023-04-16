using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Domain.Models.Auth.Requests;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[Route("auth")]
public class AuthController : BaseApiV1Controller {

    public AuthController(IMediator mediator) : base(mediator) {
    }

    [HttpGet("authorization/{token}")]
    [ProducesResponseType(typeof(AuthorizationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Authorization(string token, CancellationToken cancellationToken) {
        var command = new AuthorizationCommand(token);

        return await RequestAsync(command, cancellationToken);
    }

    [HttpPost("refresh/{refreshToken:guid}")]
    [ProducesResponseType(typeof(AuthorizationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthenticationError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Refresh(
        Guid refreshToken,
        CancellationToken cancellationToken) {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        var command = new RefreshCommand(refreshToken, ip, userAgent);

        return await RequestAsync(command, cancellationToken);
    }

    [HttpPost("registration")]
    [ProducesResponseType(typeof(AuthorizationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(RegistrationError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registration(
        [FromBody] RegistrationRequest request,
        CancellationToken cancellationToken) {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        var command = new RegistrationCommand(request, ip, userAgent);

        return await RequestAsync(command, cancellationToken);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthorizationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthorizationError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken) {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        var command = new LoginCommand(request, ip, userAgent);

        return await RequestAsync(command, cancellationToken);
    }
}