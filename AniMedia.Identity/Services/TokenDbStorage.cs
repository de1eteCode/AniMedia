using AniMedia.Identity.Contracts;
using AniMedia.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Identity.Services;

internal class TokenDbStorage : ITokenStorage {
    private readonly ApplicationIdentityDbContext _context;

    public TokenDbStorage(ApplicationIdentityDbContext context) {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task SaveRefreshToken(string username, string refreshToken, DateTime expiredAt) {
        var user = await _context.Users.SingleOrDefaultAsync(e => e.UserName!.Equals(username));

        if (user == null) {
            throw new Exception($"Not found user with '{username}' username");
        }

        await _context.RefreshTokenUsers.AddAsync(new RefreshTokenUser() {
            RefreshToken = refreshToken,
            User = user,
            DateOfExpired = expiredAt
        });

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<(bool IsFinded, KeyValuePair<string, DateTime> Pair)> TryGetRefreshToken(string username, string refreshToken) {
        var rTokenUser = await FindRefreshToken(username, refreshToken);

        bool isFinded;
        KeyValuePair<string, DateTime> pair;

        if (rTokenUser == null) {
            pair = new KeyValuePair<string, DateTime>(string.Empty, DateTime.MinValue);
            isFinded = false;
        }
        else {
            pair = new KeyValuePair<string, DateTime>(rTokenUser.RefreshToken, rTokenUser.DateOfExpired);
            isFinded = true;
        }

        return (isFinded, pair);
    }

    /// <inheritdoc/>
    public async Task<bool> TryRemoveRefreshToken(string username, string refreshToken) {
        var rTokenUser = await FindRefreshToken(username, refreshToken);

        if (rTokenUser == null) {
            return false;
        }

        _context.Entry(rTokenUser).State = EntityState.Deleted;
        await _context.SaveChangesAsync();

        return true;
    }

    private Task<RefreshTokenUser?> FindRefreshToken(string username, string refreshToken) =>
        _context.RefreshTokenUsers.FirstOrDefaultAsync(e => e.User.UserName!.Equals(username) && e.RefreshToken.Equals(refreshToken));
}