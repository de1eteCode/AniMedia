using System.Text;

namespace AniMedia.Application.Common.Models;

public class JwtSettings {
    public string Key { get; init; } = default!;

    public string Issuer { get; init; } = default!;

    public string Audience { get; init; } = default!;

    public double AccessTokenLifeTimeInMinutes { get; init; }

    public byte[] GetKeyBytes() => Encoding.UTF8.GetBytes(Key);
}