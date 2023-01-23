using AniMedia.Identity.Contracts;
using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AniMedia.Identity.Services;

internal class TokenService : ITokenService {
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings) {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    /// <inheritdoc/>
    public async Task<string> GenerateAccessToken(ApplicationUser user) {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles) {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(CustomClaimTypes.UID, user.Id.ToString()),
        }
            .Union(userClaims)
            .Union(roleClaims);

        return await GenerateAccessToken(claims);
    }

    /// <inheritdoc/>
    public Task<string> GenerateAccessToken(IEnumerable<Claim> claims) {
        var symmetricSecurityKey = new SymmetricSecurityKey(_jwtSettings.GetKeyBytes());
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
    }

    /// <inheritdoc/>
    public Task<string> GenerateRefreshToken() {
        using var rndNum = RandomNumberGenerator.Create();
        var randomBytes = new byte[_jwtSettings.RefreshTokenBytes];
        rndNum.GetBytes(randomBytes);
        return Task.FromResult(Convert.ToBase64String(randomBytes));
    }
}