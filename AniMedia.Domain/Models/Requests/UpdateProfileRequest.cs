namespace AniMedia.Domain.Models.Requests;

public class UpdateProfileRequest {
    public string? FirstName { get; set; } = default;

    public string? SecondName { get; set; } = default;
}