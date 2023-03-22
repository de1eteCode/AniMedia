using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Domain.Constants;
using AniMedia.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AniMedia.Application.Common.Services;

/// <inheritdoc/>
public class TokenService : ITokenService {
    private const string _algoritm = SecurityAlgorithms.HmacSha256;

    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly JwtSettings _jwtSettings;
    private readonly JwtBearerOptions _jwtBearerOptions;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        IOptionsMonitor<JwtBearerOptions> jwtBearerOptionsMonitor) {
        _jwtSettings = jwtSettings.Value;
        _jwtBearerOptions = jwtBearerOptionsMonitor.Get(JwtBearerDefaults.AuthenticationScheme);
        _tokenHandler = new();
    }

    /// <inheritdoc/>
    public string CreateAccessToken(UserEntity user) {
        var claims = new[] {
            new Claim(ClaimConstants.UID, user.UID.ToString()),
            new Claim(ClaimConstants.Login, user.Nickname),
            new Claim(ClaimConstants.RandomToken, Guid.NewGuid().ToString()),
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(_jwtSettings.GetKeyBytes());
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, _algoritm);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes),
            signingCredentials: signingCredentials);

        return _tokenHandler.WriteToken(jwtToken);
    }

    /// <inheritdoc/>
    public bool TryValidateAccessToken(string token, out JwtSecurityToken validatedJwtToken) {
        try {
            _tokenHandler.ValidateToken(token, _jwtBearerOptions.TokenValidationParameters, out var securityToken);

            validatedJwtToken = (JwtSecurityToken)securityToken;

            return true;
        }
        catch {
            validatedJwtToken = new JwtSecurityToken();

            return false;
        }
    }
}