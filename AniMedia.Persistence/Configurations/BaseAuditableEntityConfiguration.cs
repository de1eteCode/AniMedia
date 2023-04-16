using AniMedia.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Persistence.Configurations;

public abstract class BaseAuditableEntityConfiguration<TAuditableEntity> : IEntityTypeConfiguration<TAuditableEntity>
    where TAuditableEntity : BaseAuditableEntity {

    public virtual void Configure(EntityTypeBuilder<TAuditableEntity> builder) {
        builder.Property(e => e.CreateAt).IsRequired();
        builder.Property(e => e.LastModified).IsRequired(false);

        builder
            .HasOne(e => e.CreateBy)
            .WithMany()
            .HasForeignKey(e => e.CreateByUid)
            .IsRequired(false);
        
        builder
            .HasOne(e => e.LastModifiedBy)
            .WithMany()
            .HasForeignKey(e => e.LastModifiedByUid)
            .IsRequired(false);
    }
}