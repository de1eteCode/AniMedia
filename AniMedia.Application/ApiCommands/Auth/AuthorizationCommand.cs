using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Domain.Constants;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AniMedia.Application.ApiCommands.Auth;

/// <summary>
/// Обновление токена для сессии
/// </summary>
/// <param name="AccessToken">Токен активной сессии</param>
public record AuthorizationCommand(string AccessToken) : IRequest<Result<AuthorizationResponse>>;

public class AuthorizationCommandHandler : IRequestHandler<AuthorizationCommand, Result<AuthorizationResponse>> {
    private readonly IApplicationDbContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;

    public AuthorizationCommandHandler(
        IApplicationDbContext context,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings) {
        _context = context;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthorizationResponse>> Handle(AuthorizationCommand request, CancellationToken cancellationToken) {
        if (_tokenService.TryValidateAccessToken(request.AccessToken, out var validatedToken) == false) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Invalid token"));
        }

        var requesterUidStr = validatedToken.Claims.FirstOrDefault(e => e.Type.Equals(ClaimConstants.UID))?.Value ?? string.Empty;

        if (string.IsNullOrEmpty(requesterUidStr) || Guid.TryParse(requesterUidStr, out var requesterUid) == false) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Not found user id in token"));
        }

        var requester = await _context.Users.FirstOrDefaultAsync(e => e.UID.Equals(requesterUid), cancellationToken);

        if (requester == null) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Not found user"));
        }

        var session = await _context.Sessions.FirstOrDefaultAsync(e => e.UserUid.Equals(requesterUid) && e.AccessToken.Equals(request.AccessToken), cancellationToken);

        if (session == null) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Not found active session"));
        }

        var newAccessToken = _tokenService.CreateAccessToken(requester);

        session.UpdateAccessToken(newAccessToken, _jwtSettings.AccessTokenLifeTimeInMinutes);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<AuthorizationResponse>(new AuthorizationResponse(requester.UID, newAccessToken, session.RefreshToken));
    }
}