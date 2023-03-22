using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class SessionEntityConfiguration : IEntityTypeConfiguration<SessionEntity> {

    public void Configure(EntityTypeBuilder<SessionEntity> builder) {
        builder.HasKey(e => e.UID);
        builder.Property(e => e.UserUid).IsRequired();
        builder.Property(e => e.AccessToken).IsRequired();
        builder.Property(e => e.Ip).IsRequired();
        builder.Property(e => e.UserAgent).IsRequired();
        builder.Property(e => e.ExpiresAt).IsRequired();
        builder.Property(e => e.CreateAt).IsRequired();
        builder.Property(e => e.RefreshToken).IsRequired();
    }
}