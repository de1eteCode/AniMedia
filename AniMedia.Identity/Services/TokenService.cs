using AniMedia.Application.Models.Identity;
using AniMedia.Identity.Contracts;
using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AniMedia.Identity.Services;

internal class TokenService : ITokenService {
    private const string _algoritm = SecurityAlgorithms.HmacSha256;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly JwtSettings _jwtSettings;
    private readonly JwtBearerOptions _jwtBearerOptions;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        //ITokenStorage tokenStorage,
        IOptions<JwtSettings> jwtSettings,
        IOptionsMonitor<JwtBearerOptions> jwtBearerOptionsMonitor) {
        _userManager = userManager;
        //_tokenStorage = tokenStorage;
        _jwtSettings = jwtSettings.Value;
        _jwtBearerOptions = jwtBearerOptionsMonitor.Get(JwtBearerDefaults.AuthenticationScheme);
    }

    /// <inheritdoc/>
    public async Task<TokenPair> GenerateTokenPair(ApplicationUser user) {
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

        return await GenerateTokenPair(claims);
    }

    /// <inheritdoc/>
    public Task<TokenPair> GenerateTokenPair(IEnumerable<Claim> claims) {
        var symmetricSecurityKey = new SymmetricSecurityKey(_jwtSettings.GetKeyBytes());
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, _algoritm);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        var pair = new TokenPair() {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken = GenerateRefreshToken()
        };

        /// Todo:
        ///     Save new pair

        return Task.FromResult(pair);
    }

    /// <inheritdoc/>
    public Task<TokenPair> GenerateTokenPair(TokenPair expiredPair) {
        var principal = GetPrincipal(expiredPair.AccessToken);
        var userName = principal.Identity!.Name;

        /// Todo:
        ///     Find user in local memory and compare refresh token
        ///     If equals - save and return new pair
        ///     Else - exception

        var pair = GenerateTokenPair(principal.Claims);
        return pair;
    }

    private string GenerateRefreshToken() {
        using var rndNum = RandomNumberGenerator.Create();
        var randomBytes = new byte[_jwtSettings.RefreshTokenBytes];
        rndNum.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private ClaimsPrincipal GetPrincipal(string token) {
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(token, _jwtBearerOptions.TokenValidationParameters, out var validToken);

        if (validToken == null ||
            (validToken is JwtSecurityToken) == false ||
            ((JwtSecurityToken)validToken).Header.Alg.Equals(_algoritm, StringComparison.InvariantCultureIgnoreCase) == false) {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}