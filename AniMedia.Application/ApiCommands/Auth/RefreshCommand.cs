﻿using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AniMedia.Application.ApiCommands.Auth;
public record RefreshCommand(Guid RefreshToken, string Ip, string UserAgent) : IRequest<Result<AuthorizationResponse>>;

public class RefreshCommandHandler : IRequestHandler<RefreshCommand, Result<AuthorizationResponse>> {
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public RefreshCommandHandler(IApplicationDbContext context, ITokenService tokenService, IOptions<JwtSettings> jwtSettings) {
        _context = context;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthorizationResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken) {
        var session = await _context.Sessions.FirstOrDefaultAsync(e => e.RefreshToken.Equals(request.RefreshToken), cancellationToken);

        if (session == null) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Refresh token not found"));
        }

        if (session.IsExpired) {
            return new Result<AuthorizationResponse>(new AuthenticationError("Refresh token is expired"));
        }

        var accessToken = _tokenService.CreateAccessToken(session.User);

        var sessionExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes);

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

        return new Result<AuthorizationResponse>(new AuthorizationResponse(newSession.User, accessToken, newSession.RefreshToken));
    }
}