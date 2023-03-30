using System.Text;

namespace AniMedia.Application.Common.Models;

public class JwtSettings {

    public double AccessTokenLifeTimeInMinutes { get; init; }

    public string Audience { get; init; } = default!;

    public string Issuer { get; init; } = default!;

    public string Key { get; init; } = default!;

    public byte[] GetKeyBytes() {
        return Encoding.UTF8.GetBytes(Key);
    }
}