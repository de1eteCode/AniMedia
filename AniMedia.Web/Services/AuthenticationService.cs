﻿using AniMedia.Application.Models.Identity;
using AniMedia.Web.Models.ViewModels.Identity;
using AniMedia.Web.Services.Base;
using AniMedia.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using IAuthService = AniMedia.Web.Contracts.IAuthenticationService;

namespace AniMedia.Web.Services;

internal class AuthenticationService : BaseService, IAuthService {
    private readonly string TokenKey = nameof(TokenKey);
    private readonly string RefreshToken = nameof(RefreshToken);

    private readonly ProtectedLocalStorage _localStorage;

    public AuthenticationService(IApiClient api, ProtectedLocalStorage localStorage) : base(api) {
        _localStorage = localStorage;
    }

    public async Task<bool> Authenticate(LoginVM viewModel) {
        try {
            var request = new AuthorizationRequest() {
                UserName = viewModel.UserName,
                Password = viewModel.Password,
            };

            var responce = await _api.ApiV1AccountLoginAsync(request);

            if (responce == null ||
                string.IsNullOrEmpty(responce.Tokens.AccessToken) ||
                string.IsNullOrEmpty(responce.Tokens.RefreshToken)) {
                return false;
            }

            await _localStorage.SetAsync(TokenKey, responce.Tokens.AccessToken);
            await _localStorage.SetAsync(RefreshToken, responce.Tokens.RefreshToken);

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

    public async Task<bool> Register(RegisterVM viewModel) {
        var request = new RegistrationRequest() {
            UserName = viewModel.UserName,
            FirstName = viewModel.FirstName,
            SecondName = viewModel.SecondName,
            Email = viewModel.Email,
            Password = viewModel.Password
        };

        var responce = await _api.ApiV1AccountRegisterAsync(request);

        if (responce == null || string.IsNullOrEmpty(responce.UserName)) {
            return false;
        }

        if (viewModel.LogInAfterRegister) {
            var authResult = await Authenticate(new LoginVM() {
                UserName = viewModel.UserName,
                Password = viewModel.Password,
            });

            return authResult;
        }

        return true;
    }

    public async Task Logout() {
        await _localStorage.DeleteAsync(TokenKey);
        await _localStorage.DeleteAsync(RefreshToken);
    }

    public async Task<IEnumerable<Claim>> GetClaims() {
        ProtectedBrowserStorageResult<string?> accessToken = default;

        try {
            accessToken = await _localStorage.GetAsync<string?>(TokenKey);
        }
        catch (CryptographicException) {
            await Logout();
            return Enumerable.Empty<Claim>();
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
}