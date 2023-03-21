using AniMedia.Domain.Constants;
using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity> {

    public void Configure(EntityTypeBuilder<UserEntity> builder) {
        builder
            .HasMany(u => u.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserUid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Nickname).IsRequired();
        builder.Property(x => x.PasswordSalt).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();

        var de1ete = new UserEntity(
            nickname: "de1ete",
            passwordHash: "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==",
            passwordSalt: "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0="
        ) {
            UID = SeedDataConstants.De1eteUserUid
        };

        var common = new UserEntity(
            nickname: "common",
            passwordHash: "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==",
            passwordSalt: "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0="
        ) {
            UID = SeedDataConstants.CommonUserUid
        };

        builder.HasData(de1ete, common);
    }
}