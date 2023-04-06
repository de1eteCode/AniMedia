using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using AniMedia.Domain.Constants;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AniMedia.Application.Common.Services;

/// <inheritdoc />
public class TokenService : ITokenService {
    private const string _algoritm = SecurityAlgorithms.HmacSha256;
    private readonly JwtBearerOptions _jwtBearerOptions;
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeService _timeService;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        IOptionsMonitor<JwtBearerOptions> jwtBearerOptionsMonitor, 
        IDateTimeService timeService) {
        _timeService = timeService;
        _jwtSettings = jwtSettings.Value;
        _jwtBearerOptions = jwtBearerOptionsMonitor.Get(JwtBearerDefaults.AuthenticationScheme);
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    /// <inheritdoc />
    public string CreateAccessToken(UserEntity user) {
        var claims = new[] {
            new Claim(ClaimConstants.UID, user.UID.ToString()),
            new Claim(ClaimConstants.Login, user.Nickname),
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(ClaimConstants.RandomToken, Guid.NewGuid().ToString())
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(_jwtSettings.GetKeyBytes());
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, _algoritm);

        var jwtToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: _timeService.Now.AddMinutes(_jwtSettings.AccessTokenLifeTimeInMinutes),
            signingCredentials: signingCredentials);

        return _tokenHandler.WriteToken(jwtToken);
    }

    /// <inheritdoc />
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