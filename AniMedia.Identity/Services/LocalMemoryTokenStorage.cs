using AniMedia.Identity.Contracts;
using Hanssens.Net;

namespace AniMedia.Identity.Services;

internal class LocalMemoryTokenStorage : ITokenStorage {
    private readonly LocalStorage _storage;

    public LocalMemoryTokenStorage(LocalStorageConfiguration storageConfiguration) {
        _storage = new LocalStorage(storageConfiguration);
    }

    /// <inheritdoc/>
    public Task SaveRefreshToken(string refreshToken, DateTime expiredAt) {
        _storage.Store(refreshToken, expiredAt);
        _storage.Persist();
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<bool> TryGetRefreshToken(string refreshToken, out KeyValuePair<string, DateTime> pair) {
        if (_storage.Exists(refreshToken)) {
            var dt = _storage.Get<DateTime>(refreshToken);
            pair = new KeyValuePair<string, DateTime>(refreshToken, dt);
            return Task.FromResult(true);
        }

        pair = new KeyValuePair<string, DateTime>(string.Empty, DateTime.MinValue);

        return Task.FromResult(false);
    }

    /// <inheritdoc/>
    public Task<bool> TryRemoveRefreshToken(string refreshToken) {
        if (_storage.Exists(refreshToken)) {
            _storage.Remove(refreshToken);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}