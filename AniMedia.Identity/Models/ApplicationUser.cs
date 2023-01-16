using Microsoft.AspNetCore.Identity;

namespace AniMedia.Identity.Models;

internal class ApplicationUser : IdentityUser<Guid> {
    public string FirstName { get; set; } = default!;

    public string SecondName { get; set; } = default!;
}