using System.Text;

namespace AniMedia.Identity.Models;

internal class JwtSettings {
    public required string Key { get; init; }

    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public required double AccessTokenLifeTimeInMinutes { get; init; }

    public required int RefreshTokenBytes { get; init; }

    public required int RefreshTokenLifeTimeInMinutes { get; init; }

    public byte[] GetKeyBytes() => Encoding.UTF8.GetBytes(Key);
}