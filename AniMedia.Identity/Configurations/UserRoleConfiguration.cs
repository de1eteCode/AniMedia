using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Identity.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>> {

    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder) {
        builder.HasData(
            new IdentityUserRole<Guid>() {
                RoleId = RoleConfiguration.ADMIN.UID,
                UserId = UserConfiguration.ADMIN_UID
            });
    }
}