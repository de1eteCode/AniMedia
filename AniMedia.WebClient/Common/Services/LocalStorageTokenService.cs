using AniMedia.WebClient.Common.Contracts;
using Blazored.LocalStorage;

namespace AniMedia.WebClient.Common.Services;

public class LocalStorageTokenService : ITokenService {

    private readonly ILocalStorageService _localStorage;
    public readonly string RefreshToken = nameof(RefreshToken);
    public readonly string Token = nameof(Token);

    public LocalStorageTokenService(ILocalStorageService localStorage) {
        _localStorage = localStorage;
    }

    public ValueTask DeleteRefreshTokenAsync(CancellationToken cancellationToken) {
        return _localStorage.RemoveItemAsync(RefreshToken, cancellationToken);
    }

    public ValueTask DeleteTokenAsync(CancellationToken cancellationToken) {
        return _localStorage.RemoveItemAsync(Token, cancellationToken);
    }

    public ValueTask<Guid> GetRefreshTokenAsync(CancellationToken cancellationToken) {
        return _localStorage.GetItemAsync<Guid>(RefreshToken, cancellationToken);
    }

    public ValueTask<string> GetTokenAsync(CancellationToken cancellationToken) {
        return _localStorage.GetItemAsync<string>(Token, cancellationToken);
    }

    public ValueTask SetRefreshTokenAsync(Guid refreshtoken, CancellationToken cancellationToken) {
        return _localStorage.SetItemAsync(RefreshToken, refreshtoken, cancellationToken);
    }

    public ValueTask SetTokenAsync(string token, CancellationToken cancellationToken) {
        return _localStorage.SetItemAsStringAsync(Token, token, cancellationToken);
    }
}