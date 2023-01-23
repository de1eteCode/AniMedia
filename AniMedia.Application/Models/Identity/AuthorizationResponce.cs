namespace AniMedia.Application.Models.Identity;

public class AuthorizationResponce {
    public required string UserName { get; init; }

    public required string Token { get; init; }

    public required string RefreshToken { get; init; }
}