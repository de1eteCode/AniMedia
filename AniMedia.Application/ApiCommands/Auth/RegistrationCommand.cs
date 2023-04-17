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
/// Регистрация нового пользователя с последующей авторизацией (создание сессии)
/// </summary>
/// <param name="Nickname">Никнейм</param>
/// <param name="Password">Пароль</param>
/// <param name="Ip">Ip адрес</param>
/// <param name="UserAgent">Юзер агент</param>
public record RegistrationCommand(string Nickname, string Password, string Ip, string UserAgent) : IRequest<Result<AuthorizationResponse>>;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Result<AuthorizationResponse>> {
    private readonly IApplicationDbContext _context;
    private readonly IHashService _hashService;
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;
    private readonly IDateTimeService _timeService;

    public RegistrationCommandHandler(
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

    public async Task<Result<AuthorizationResponse>> Handle(RegistrationCommand request, CancellationToken cancellationToken) {
        var isUserByNicknameExists = await _context.Users.AnyAsync(e => e.Nickname.Equals(request.Nickname), cancellationToken);

        if (isUserByNicknameExists) {
            return new Result<AuthorizationResponse>(new RegistrationError("User already exists", ErrorCodesConstants.Exist));
        }

        var passHash = _hashService.Hmacsha512CryptoHash(request.Password, out var passSalt);

        var newUser = new UserEntity(request.Nickname, passHash, passSalt);

        var accessToken = _tokenService.CreateAccessToken(newUser);

        var sessionExpiresAt = _timeService.Now.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes);

        var session = new SessionEntity(
            newUser.UID,
            accessToken,
            request.Ip,
            request.UserAgent,
            sessionExpiresAt);

        _context.Users.Add(newUser);
        _context.Sessions.Add(session);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<AuthorizationResponse>(new AuthorizationResponse(newUser.UID, accessToken, session.RefreshToken));
    }
}

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand> {

    public RegistrationCommandValidator() {
        RuleFor(e => e.Nickname).NotEmpty();
        RuleFor(e => e.Password).NotEmpty();
        RuleFor(e => e.UserAgent).NotEmpty();
        RuleFor(e => e.Ip).NotEmpty();
    }
}