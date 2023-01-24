using AniMedia.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Identity.Configurations;

internal class RefreshTokenUserConfiguration : BaseEntityTypeConfiguration<RefreshTokenUser> {

    public override void Configure(EntityTypeBuilder<RefreshTokenUser> builder) {
        builder
            .ToTable(nameof(RefreshTokenUser) + 's');

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(e => e.RefreshToken)
            .IsRequired();

        builder
            .Property(e => e.DateOfExpired)
            .IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}