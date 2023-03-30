using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AniMedia.WebClient.Common.Contracts;

namespace AniMedia.WebClient.Common.Services;

public class JwtReadService : IJwtTokenReadService {
    private readonly JwtSecurityTokenHandler _jwtHandler;

    public JwtReadService() {
        _jwtHandler = new JwtSecurityTokenHandler();
    }

    public IEnumerable<string> GetAudiences(string token) {
        if (TryParseJwtToken(token, out var securityToken)) return securityToken!.Audiences;

        return Enumerable.Empty<string>();
    }

    public IEnumerable<Claim> GetClaims(string token) {
        if (TryParseJwtToken(token, out var securityToken)) return securityToken!.Claims;

        return Enumerable.Empty<Claim>();
    }

    public DateTime GetExpirationDate(string token) {
        if (TryParseJwtToken(token, out var securityToken)) return securityToken!.ValidTo;

        return DateTime.MinValue;
    }

    private bool TryParseJwtToken(string token, out JwtSecurityToken? securityToken) {
        try {
            securityToken = _jwtHandler.ReadJwtToken(token);
            return true;
        }
        catch {
            securityToken = default;
            return false;
        }
    }
}