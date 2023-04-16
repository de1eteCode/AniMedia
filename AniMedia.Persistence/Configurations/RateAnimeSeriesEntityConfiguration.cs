using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public class RateAnimeSeriesEntityConfiguration : BaseAuditableEntityConfiguration<RateAnimeSeriesEntity> {

    public override void Configure(EntityTypeBuilder<RateAnimeSeriesEntity> builder) {
        builder.HasKey(e => new { e.UID, e.AnimeSeriesUid, e.UserUid });
        builder.Property(e => e.Rate).IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserUid)
            .IsRequired();
        
        builder
            .HasOne(e => e.AnimeSeries)
            .WithMany()
            .HasForeignKey(e => e.AnimeSeriesUid)
            .IsRequired();
        
        base.Configure(builder);
    }
}