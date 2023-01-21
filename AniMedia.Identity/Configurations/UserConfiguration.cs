using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Identity.Configurations;

internal class UserConfiguration : BaseEntityTypeConfiguration<ApplicationUser> {
    internal static readonly Guid ADMIN_UID = Guid.Parse("A6330A68-A4E3-466C-9A03-6B45C093F90F");

    public override void Configure(EntityTypeBuilder<ApplicationUser> builder) {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser() {
                Id = ADMIN_UID,
                Email = "admin@localhost.com",
                NormalizedEmail = _lookupNormalizer.NormalizeEmail("admin@localhost.com"),
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = _lookupNormalizer.NormalizeName("admin"),
                FirstName = "Admin",
                SecondName = "",
                PasswordHash = hasher.HashPassword(null!, "P@ssw0rd")
            });
    }
}