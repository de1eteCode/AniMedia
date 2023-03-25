using AniMedia.Domain.Models.Requests;
using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.ViewModels;

namespace AniMedia.WebClient.Common.Services;

public class AuthenticationService : IAuthenticationService {
    private readonly IApiClient _apiClient;
    private readonly ITokenService _tokenService;

    public AuthenticationService(IApiClient apiClient, ITokenService tokenService) {
        _apiClient = apiClient;
        _tokenService = tokenService;
    }

    public async Task<bool> Login(LoginVM viewModel) {
        var request = new LoginRequest() {
            Nickname = viewModel.Nickname,
            Password = viewModel.Password
        };

        var responce = await _apiClient.ApiV1AuthLoginAsync(request);

        if (responce == null || string.IsNullOrEmpty(responce.AccessToken)) {
            return false;
        }

        await _tokenService.SetTokenAsync(responce.AccessToken);
        await _tokenService.SetRefreshTokenAsync(responce.RefreshToken);

        return true;
    }

    public async Task Logout() {
        var currentToken = await _tokenService.GetTokenAsync();

        if (string.IsNullOrEmpty(currentToken)) {
            return;
        }

        var currentSession = await _apiClient.ApiV1AuthSessionsGetAsync(currentToken);

        if (currentSession != null) {
            await _apiClient.ApiV1AuthRemovesessionAsync(currentSession.Uid);
        }

        await _tokenService.DeleteTokenAsync();
        await _tokenService.DeleteRefreshTokenAsync();
    }

    public async Task<bool> Registration(RegistrationVM viewModel) {
        var request = new RegistrationRequest() {
            Nickname = viewModel.Nickname,
            Password = viewModel.Password
        };

        var responce = await _apiClient.ApiV1AuthRegistrationAsync(request);

        if (responce == null || string.IsNullOrEmpty(responce.AccessToken)) {
            return false;
        }

        await _tokenService.SetTokenAsync(responce.AccessToken);
        await _tokenService.SetRefreshTokenAsync(responce.RefreshToken);

        return true;
    }
}