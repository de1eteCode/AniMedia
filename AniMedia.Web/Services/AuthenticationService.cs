using AniMedia.Domain.Models.Requests;
using AniMedia.Web.Models.ViewModels.Identity;
using AniMedia.Web.Services.Base;
using AniMedia.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthService = AniMedia.Web.Contracts.IAuthenticationService;

namespace AniMedia.Web.Services;

public class AuthenticationService : BaseService, IAuthService {
    private readonly string TokenKey = nameof(TokenKey);
    private readonly string RefreshToken = nameof(RefreshToken);

    private readonly ProtectedLocalStorage _localStorage;

    public AuthenticationService(IApiClient api, ProtectedLocalStorage localStorage) : base(api) {
        _localStorage = localStorage;
    }

    public Task<bool> Authenticate() {
        /// Get current token, check on server side and save new access token

        try {
        }
        catch (Exception) {
        }

        throw new NotImplementedException();
    }

    public async Task<bool> Register(RegisterVM viewModel) {
        var request = new RegistrationRequest() {
            Nickname = viewModel.UserName,
            Password = viewModel.Password
        };

        var responce = await _api.ApiV1AuthRegistrationAsync(request);

        if (responce == null || string.IsNullOrEmpty(responce.AccessToken)) {
            return false;
        }

        await _localStorage.SetAsync(TokenKey, responce.AccessToken);
        await _localStorage.SetAsync(RefreshToken, responce.RefreshToken);

        return true;
    }

    public async Task<bool> Login(LoginVM viewModel) {
        try {
            var request = new LoginRequest() {
                Nickname = viewModel.UserName,
                Password = viewModel.Password
            };

            var responce = await _api.ApiV1AuthLoginAsync(request);

            if (responce == null ||
                string.IsNullOrEmpty(responce.AccessToken) ||
                responce.RefreshToken != Guid.Empty) {
                return false;
            }

            await _localStorage.SetAsync(TokenKey, responce.AccessToken);
            await _localStorage.SetAsync(RefreshToken, responce.RefreshToken);

            return true;
        }
        catch (Exception) {
#if DEBUG
            throw;
#else
            return false;
#endif
        }
    }

    public async Task<IEnumerable<Claim>> GetClaims() {
        ProtectedBrowserStorageResult<string?> accessToken = default;

        try {
            accessToken = await _localStorage.GetAsync<string?>(TokenKey);
        }
        catch (Exception) {
            throw;
        }

        if (string.IsNullOrEmpty(accessToken.Value)) {
            return Enumerable.Empty<Claim>();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenData = tokenHandler.ReadJwtToken(accessToken.Value);

        return tokenData.Claims;
    }

    public Task<bool> IsSignedIn() {
        /// Verify time expire and try get new token by refresh
        throw new NotImplementedException();
    }

    public async Task Logout() {
        /// Send to server remove session request and delete jwt token on client

        ProtectedBrowserStorageResult<string?> accessToken = default;

        try {
            accessToken = await _localStorage.GetAsync<string?>(TokenKey);

            if (string.IsNullOrEmpty(accessToken.Value) == false) {
                await _api.ApiV1AuthSessionsGetAsync(accessToken.Value);
            }
        }
        catch (Exception) {
#if DEBUG
            throw;
#else
            return false;
#endif
        }

        await _localStorage.DeleteAsync(TokenKey);
        await _localStorage.DeleteAsync(RefreshToken);
    }
}