using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AniMedia.Identity.Configurations;

internal abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class {
    protected readonly ILookupNormalizer _lookupNormalizer = new UpperInvariantLookupNormalizer();

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}