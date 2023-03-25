using System.Security.Claims;

namespace AniMedia.WebClient.Common.Contracts;

public interface IJwtTokenReadService {

    public IEnumerable<string> GetAudiences(string token);

    public DateTime GetExpirationDate(string token);

    public IEnumerable<Claim> GetClaims(string token);
}