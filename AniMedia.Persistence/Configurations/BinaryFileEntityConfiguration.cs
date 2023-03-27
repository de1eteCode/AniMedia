using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class BinaryFileEntityConfiguration : IEntityTypeConfiguration<BinaryFileEntity> {

    public void Configure(EntityTypeBuilder<BinaryFileEntity> builder) {
        builder.HasKey(e => e.UID);
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.PathFile).IsRequired();
        builder.Property(e => e.ContentType).IsRequired();
        builder.Property(e => e.Length).IsRequired();
        builder.Property(e => e.Hash).IsRequired();
    }
}