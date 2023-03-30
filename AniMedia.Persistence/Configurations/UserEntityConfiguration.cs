using AniMedia.Domain.Constants;
using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity> {

    public void Configure(EntityTypeBuilder<UserEntity> builder) {
        builder.HasKey(e => e.UID);

        builder
            .HasMany(e => e.Sessions)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserUid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Nickname).IsRequired();
        builder.Property(e => e.PasswordSalt).IsRequired();
        builder.Property(e => e.PasswordHash).IsRequired();

        var de1ete = new UserEntity(
            "de1ete",
            "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==",
            "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0="
        ) {
            UID = SeedDataConstants.De1eteUserUid
        };

        var common = new UserEntity(
            "common",
            "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==",
            "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0="
        ) {
            UID = SeedDataConstants.CommonUserUid
        };

        builder.HasData(de1ete, common);
    }
}