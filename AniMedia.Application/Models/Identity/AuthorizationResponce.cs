namespace AniMedia.Application.Models.Identity;

public class AuthorizationResponce {
    public required string UserName { get; init; }

    public required TokenPair Tokens { get; init; }
}