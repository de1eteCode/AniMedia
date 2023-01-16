namespace AniMedia.Application.Models.Identity;

public class RegistrationRequest {
    public required string UserName { get; init; }

    public required string FirstName { get; init; }

    public required string SecondName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}