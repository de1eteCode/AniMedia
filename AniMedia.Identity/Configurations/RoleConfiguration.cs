using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Identity.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>> {

    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder) {
        builder.HasData(new IdentityRole<Guid> {
            Id = new Guid(""),
            Name = "",
            NormalizedName = ""
        });

        throw new NotImplementedException();
    }
}