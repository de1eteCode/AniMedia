using AniMedia.WebClient.Common.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AniMedia.WebClient.Common.Providers;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly ITokenService _tokenService;
    private readonly IJwtTokenReadService _jwtTokenReadService;

    public JwtAuthenticationStateProvider(ITokenService tokenService, IJwtTokenReadService jwtTokenReadService) {
        _tokenService = tokenService;
        _jwtTokenReadService = jwtTokenReadService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var currentToken = await _tokenService.GetTokenAsync(default);

        if (string.IsNullOrEmpty(currentToken)) {
            return AnonymusState();
        }

        var expiredAt = _jwtTokenReadService.GetExpirationDate(currentToken);

        if (expiredAt < DateTime.UtcNow) {
            return AnonymusState();
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(_jwtTokenReadService.GetClaims(currentToken), "Bearer")));
    }

    public void NotifyAuthenticationStateChanged() {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static AuthenticationState AnonymusState() => new(new ClaimsPrincipal());
}