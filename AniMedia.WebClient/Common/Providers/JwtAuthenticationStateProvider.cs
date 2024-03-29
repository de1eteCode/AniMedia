﻿using System.Security.Claims;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Store;
using AniMedia.WebClient.Common.ViewModels;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace AniMedia.WebClient.Common.Providers;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider {
    private const string Bearer = nameof(Bearer);

    private readonly IJwtTokenReadService _jwtTokenReadService;
    private readonly ITokenService _tokenService;
    private readonly IApiClient _apiClient;
    private readonly ILogger<JwtAuthenticationStateProvider> _logger;
    private readonly IDispatcher _dispatcher;

    public JwtAuthenticationStateProvider(
        IJwtTokenReadService jwtTokenReadService,
        ITokenService tokenService,
        IApiClient apiClient,
        ILogger<JwtAuthenticationStateProvider> logger,
        IDispatcher dispatcher) {
        _jwtTokenReadService = jwtTokenReadService;
        _tokenService = tokenService;
        _apiClient = apiClient;
        _logger = logger;
        _dispatcher = dispatcher;
    }

    /// <inheritdoc/>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var currentToken = await _tokenService.GetTokenAsync();

        if (string.IsNullOrEmpty(currentToken)) {
            return AnonymusState();
        }

        var authResult = await Auth(currentToken);

        if (authResult == false) {
            var currentRefreshToken = await _tokenService.GetRefreshTokenAsync();

            if (currentRefreshToken == default ||
                await AuthRefresh(currentRefreshToken) == false) {
                return AnonymusState();
            }
        }
        
        // load user state
        _dispatcher.Dispatch(new UserInfoActions.LoadUserInfo());

        return new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(_jwtTokenReadService.GetClaims(currentToken), Bearer)));
    }

    /// <summary>
    /// Аутентификация пользователя по токену
    /// </summary>
    /// <param name="token">Токен пользователя</param>
    /// <returns>True - Пользователь успешно аутентифицирован, иначе False</returns>
    private async Task<bool> Auth(string token) {
        try {
            var authResult = await _apiClient.AuthAuthorizationAsync(token);

            if (authResult == null) {
                return false;
            }

            await _tokenService.SetTokenAsync(authResult.AccessToken);
            await _tokenService.SetRefreshTokenAsync(authResult.RefreshToken);

            return true;
        }
        catch (ApiClientException<AuthenticationError> ex) {
            _logger.LogError(ex, "Ошибка при аутентификации по токену");
            await _tokenService.DeleteTokenAsync();
        }

        return false;
    }

    /// <summary>
    /// Аутентификация пользователя по рефреш токену
    /// </summary>
    /// <param name="refreshToken">Рефреш токен</param>
    /// <returns>True - Пользователь аутентифицирован, иначе False</returns>
    private async Task<bool> AuthRefresh(Guid refreshToken) {
        try {
            var refreshResult = await _apiClient.AuthRefreshAsync(refreshToken);

            if (refreshResult == null) {
                return false;
            }

            await _tokenService.SetTokenAsync(refreshResult.AccessToken);
            await _tokenService.SetRefreshTokenAsync(refreshResult.RefreshToken);

            return true;
        }
        catch (ApiClientException<EntityNotFoundError>) {
            await _tokenService.DeleteTokenAsync();
            await _tokenService.DeleteRefreshTokenAsync();
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Ошибка при аутентификации по рефреш токену");
            await _tokenService.DeleteTokenAsync();
            await _tokenService.DeleteRefreshTokenAsync();
        }

        return false;
    }

    /// <summary>
    /// Уведомление о изменении состояния аутентификации пользователя
    /// </summary>
    public void NotifyAuthenticationStateChanged() {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Создание анонимного состояния
    /// </summary>
    private static AuthenticationState AnonymusState() {
        return new AuthenticationState(new ClaimsPrincipal());
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="viewModel">Данные для регистрации</param>
    /// <returns>True - Пользователь успешно зарегистрирован и авторизирован, иначе False</returns>
    public async Task<bool> Registration(RegistrationVM viewModel) {
        var responce = await _apiClient.AuthRegistrationAsync(viewModel.Nickname, viewModel.Password);

        if (responce == null || string.IsNullOrEmpty(responce.AccessToken)) {
            return false;
        }

        await _tokenService.SetTokenAsync(responce.AccessToken);
        await _tokenService.SetRefreshTokenAsync(responce.RefreshToken);

        NotifyAuthenticationStateChanged();

        return true;
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="viewModel">Данные для авторизации</param>
    /// <returns>True - Пользователь успешно авторизирован</returns>
    public async Task<bool> Login(LoginVM viewModel) {
        var response = await _apiClient.AuthLoginAsync(viewModel.Nickname, viewModel.Password);

        if (response == null || string.IsNullOrEmpty(response.AccessToken)) {
            return false;
        }

        await _tokenService.SetTokenAsync(response.AccessToken);
        await _tokenService.SetRefreshTokenAsync(response.RefreshToken);

        NotifyAuthenticationStateChanged();

        return true;
    }

    /// <summary>
    /// Деавторизация пользователя
    /// </summary>
    public async Task Logout() {
        var currentToken = await _tokenService.GetTokenAsync();

        if (string.IsNullOrEmpty(currentToken)) {
            return;
        }

        SessionDto? currentSession = default;

        try {
            currentSession = await _apiClient.ApiV1SessionAsync(currentToken);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Ошибка при выходе пользователя");
        }

        if (currentSession != null) {
            await _apiClient.ApiV1SessionRemoveAsync(currentSession.Uid);
        }

        await _tokenService.DeleteTokenAsync();
        await _tokenService.DeleteRefreshTokenAsync();

        NotifyAuthenticationStateChanged();
    }
}