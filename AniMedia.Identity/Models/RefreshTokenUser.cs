namespace AniMedia.Identity.Models;

internal class RefreshTokenUser {
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string RefreshToken { get; set; } = default!;

    public DateTime DateOfExpired { get; set; } = default!;

    public ApplicationUser User { get; set; } = default!;
}