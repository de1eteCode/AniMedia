using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AniMedia.Application.Common.Interfaces;

public interface IApplicationDbContext {
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<SessionEntity> Sessions { get; set; }

    public DbSet<BinaryFileEntity> BinaryFiles { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}