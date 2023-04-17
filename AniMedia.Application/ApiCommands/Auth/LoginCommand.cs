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
/// Авторизация пользователя в системе
/// </summary>
/// <param name="Nickname">Никнейм</param>
/// <param name="Password">Пароль</param>
/// <param name="Ip">Ip адрес</param>
/// <param name="UserAgent">Юзер агент</param>
public record LoginCommand(string Nickname, string Password, string Ip, string UserAgent) : IRequest<Result<AuthorizationResponse>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthorizationResponse>> {
    private readonly IApplicationDbContext _context;
    private readonly IHashService _hashService;
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeService _timeService;

    public LoginCommandHandler(
        IApplicationDbContext context,
        ITokenService tokenService,
        IHashService hashService,
        IOptions<JwtSettings> jwtSettings,
        IDateTimeService timeService) {
        _context = context;
        _tokenService = tokenService;
        _hashService = hashService;
        _timeService = timeService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthorizationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var requester = await _context.Users.FirstOrDefaultAsync(e => e.Nickname.Equals(request.Nickname), cancellationToken);

        if (requester == null) {
            return new Result<AuthorizationResponse>(new AuthorizationError("User does not exists", ErrorCodesConstants.NotFoundUser));
        }

        var passHash = _hashService.Hmacsha512CryptoHashWithSalt(request.Password, requester.PasswordSalt);

        if (requester.PasswordHash.Equals(passHash) == false) {
            return new Result<AuthorizationResponse>(new AuthorizationError("Password is wrong", ErrorCodesConstants.AuthInvalidPassword));
        }

        var accessToken = _tokenService.CreateAccessToken(requester);

        var sessionExpiresAt = _timeService.Now.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes);

        var session = new SessionEntity(
            requester.UID,
            accessToken,
            request.Ip,
            request.UserAgent,
            sessionExpiresAt);

        _context.Sessions.Add(session);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<AuthorizationResponse>(new AuthorizationResponse(requester.UID, accessToken, session.RefreshToken));
    }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand> {

    public LoginCommandValidator() {
        RuleFor(e => e.Nickname).NotEmpty();
        RuleFor(e => e.Password).NotEmpty();
        RuleFor(e => e.UserAgent).NotEmpty();
        RuleFor(e => e.Ip).NotEmpty();
    }
}