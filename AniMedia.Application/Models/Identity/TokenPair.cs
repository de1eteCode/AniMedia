namespace AniMedia.Application.Models.Identity;

public class TokenPair {
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }
}