namespace AniMedia.Application.Models.Identity;

public class AuthorizationRequest {
    public required string UserName { get; init; }

    public required string Password { get; init; }
}