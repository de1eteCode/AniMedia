using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Identity.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>> {
    internal static readonly StaticRoleKeyNamePair ADMIN = new(Guid.Parse("C4E2F893-6C52-487F-9553-9306395F282F"), "Администратор");
    internal static readonly StaticRoleKeyNamePair USER = new(Guid.Parse("8238CA98-C992-46D8-9CBA-90AF4425BE5B"), "Пользователь");

    private readonly ILookupNormalizer _lookupNormalizer = new UpperInvariantLookupNormalizer();

    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder) {
        builder.HasData(
            new IdentityRole<Guid> {
                Id = ADMIN.UID,
                Name = ADMIN.Name,
                NormalizedName = _lookupNormalizer.NormalizeName(ADMIN.Name)
            },
            new IdentityRole<Guid>() {
                Id = USER.UID,
                Name = USER.Name,
                NormalizedName = _lookupNormalizer.NormalizeName(USER.Name)
            });
        ;
    }

    internal record StaticRoleKeyNamePair(Guid UID, string Name);
}