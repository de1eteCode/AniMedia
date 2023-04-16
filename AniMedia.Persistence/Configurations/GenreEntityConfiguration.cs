using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class GenreEntityConfiguration : BaseAuditableEntityConfiguration<GenreEntity> {

    public override void Configure(EntityTypeBuilder<GenreEntity> builder) {
        builder.HasKey(e => e.UID);
        builder
            .Property(e => e.Name)
            .HasMaxLength(64)
            .IsRequired();
        
        base.Configure(builder);
    }
}