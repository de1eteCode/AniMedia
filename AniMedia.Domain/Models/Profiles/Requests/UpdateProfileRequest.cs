namespace AniMedia.Domain.Models.Profiles.Requests;

public class UpdateProfileRequest {
    public string? FirstName { get; set; } = default;

    public string? SecondName { get; set; } = default;
}