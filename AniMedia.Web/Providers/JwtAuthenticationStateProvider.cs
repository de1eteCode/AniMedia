using AniMedia.Web.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AniMedia.Web.Providers;

/// https://stackoverflow.com/questions/61151136/blazor-custom-authentication-provider-error

internal class JwtAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly ClaimsIdentity _anonymous;
    private readonly IAuthenticationService _authenticationService;

    public JwtAuthenticationStateProvider(IAuthenticationService authenticationService) {
        _authenticationService = authenticationService;
        _anonymous = new ClaimsIdentity();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var auth = await _authenticationService.Authenticate();

        if (auth == false) {
            return new AuthenticationState(new ClaimsPrincipal(_anonymous));
        }

        var claims = await _authenticationService.GetClaims();

        ClaimsIdentity identity = default!;

        if (claims.Any()) {
            identity = new ClaimsIdentity(claims, "Bearer");
        }
        else {
            identity = _anonymous;
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public void NotifyAuthenticationStateChanged() {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}