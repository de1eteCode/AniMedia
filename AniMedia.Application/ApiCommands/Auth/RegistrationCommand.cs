﻿using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AniMedia.Application.ApiCommands.Auth;

public record RegistrationCommand(string Nickname, string Password, string Ip, string UserAgent) : IRequest<Result<AuthorizationResponse>>;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Result<AuthorizationResponse>> {
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;
    private readonly JwtSettings _jwtSettings;

    public RegistrationCommandHandler(IApplicationDbContext context, ITokenService tokenService, IHashService hashService, IOptions<JwtSettings> jwtSettings) {
        _context = context;
        _tokenService = tokenService;
        _hashService = hashService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthorizationResponse>> Handle(RegistrationCommand request, CancellationToken cancellationToken) {
        var isUserByNicknameExists = await _context.Users.AnyAsync(e => e.Nickname.Equals(request.Nickname, StringComparison.OrdinalIgnoreCase), cancellationToken);

        if (isUserByNicknameExists) {
            return new Result<AuthorizationResponse>(new RegistrationError("User already exists"));
        }

        var passHash = _hashService.Hmacsha512CryptoHash(request.Password, out var passSalt);

        var newUser = new UserEntity(request.Nickname, passHash, passSalt);

        var accessToken = _tokenService.CreateAccessToken(newUser);

        var sessionExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes);

        var session = new SessionEntity(
            newUser.UID,
            accessToken,
            request.Ip,
            request.UserAgent,
            sessionExpiresAt);

        _context.Users.Add(newUser);
        _context.Sessions.Add(session);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<AuthorizationResponse>(new AuthorizationResponse(newUser, accessToken, session.RefreshToken));
    }
}