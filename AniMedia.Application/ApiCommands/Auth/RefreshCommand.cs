using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Domain.Constants;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Interfaces;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AniMedia.Application.ApiCommands.Auth;

/// <summary>
/// Обновление пары токенов и открытие новой сессии, с закрытием старой
/// </summary>
/// <param name="RefreshToken">Рефреш токен</param>
/// <param name="Ip">Ip адрес</param>
/// <param name="UserAgent">Юзер агент</param>
public record RefreshCommand(Guid RefreshToken, string Ip, string UserAgent) : IRequest<Result<AuthorizationResponse>>;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result<AuthorizationResponse>> {
    private readonly IApplicationDbContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeService _timeService;

    public RefreshCommandHandler(
        IApplicationDbContext context,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings,
        IDateTimeService timeService) {
        _context = context;
        _tokenService = tokenService;
        _timeService = timeService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthorizationResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken) {
        // Используется SingleOrDefault для вызова исключения,
        // если по магическим обстоятельствам появится 2 сессии с одинаковым рефреш токеном
        var session = await _context.Sessions
            .SingleOrDefaultAsync(e => e.RefreshToken.Equals(request.RefreshToken), cancellationToken);

        if (session == null) {
            return new Result<AuthorizationResponse>(new EntityNotFoundError("Refresh token not found", ErrorCodesConstants.NotFound));
        }

        if (session.IsExpired) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Refresh token is expired", ErrorCodesConstants.AuthExpired));
        }

        var accessToken = _tokenService.CreateAccessToken(session.User);

        var sessionExpiresAt = _timeService.Now.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes);

        var newSession = new SessionEntity(
            session.User.UID,
            accessToken,
            request.Ip,
            request.UserAgent,
            sessionExpiresAt);

        _context.Sessions.Remove(session);
        _context.Sessions.Add(newSession);

        await _context.SaveChangesAsync(cancellationToken);

        await _context.Entry(newSession).Reference(e => e.User).LoadAsync(cancellationToken);

        return new Result<AuthorizationResponse>(new AuthorizationResponse(newSession.User.UID, accessToken, newSession.RefreshToken));
    }
}

public class RefreshCommandValidator : AbstractValidator<RefreshCommand> {

    public RefreshCommandValidator() {
        RuleFor(e => e.RefreshToken).NotEmpty();
        RuleFor(e => e.Ip).NotEmpty();
        RuleFor(e => e.UserAgent).NotEmpty();
    }
}