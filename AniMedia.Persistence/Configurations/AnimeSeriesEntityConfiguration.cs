using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class AnimeSeriesEntityConfiguration : BaseAuditableEntityConfiguration<AnimeSeriesEntity> {

    public override void Configure(EntityTypeBuilder<AnimeSeriesEntity> builder) {
        builder.HasKey(e => e.UID);

        builder
            .Property(e => e.Name)
            .HasMaxLength(512)
            .IsRequired();

        builder
            .Property(e => e.EnglishName)
            .HasMaxLength(512)
            .IsRequired();

        builder
            .Property(e => e.JapaneseName)
            .HasMaxLength(512)
            .IsRequired();
        
        base.Configure(builder);
    }
}