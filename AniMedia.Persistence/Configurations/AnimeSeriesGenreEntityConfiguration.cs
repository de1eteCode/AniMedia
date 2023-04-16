using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class AnimeSeriesGenreEntityConfiguration : BaseAuditableEntityConfiguration<AnimeSeriesGenreEntity> {

    public override void Configure(EntityTypeBuilder<AnimeSeriesGenreEntity> builder) {
        builder.HasKey(e => new { e.UID, e.GenreUid, e.AnimeSeriesUid });

        builder
            .HasOne(e => e.Genre)
            .WithMany()
            .HasForeignKey(e => e.GenreUid)
            .IsRequired();
        
        builder
            .HasOne(e => e.AnimeSeries)
            .WithMany()
            .HasForeignKey(e => e.AnimeSeriesUid)
            .IsRequired();
        
        base.Configure(builder);
    }
}