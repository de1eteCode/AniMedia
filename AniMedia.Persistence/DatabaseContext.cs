using System.Reflection; 
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Persistence;

public class DatabaseContext : DbContext, IApplicationDbContext {

    private readonly AuditableEntitySaveChangesInterceptor _entitySaveChangesInterceptor;

    public DatabaseContext(
        DbContextOptions options,
        AuditableEntitySaveChangesInterceptor entitySaveChangesInterceptor)
        : base(options) {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        _entitySaveChangesInterceptor = entitySaveChangesInterceptor;
    }

    public virtual DbSet<BinaryFileEntity> BinaryFiles { get; set; } = default!;

    public virtual DbSet<SessionEntity> Sessions { get; set; } = default!;

    public virtual DbSet<UserEntity> Users { get; set; } = default!;

    public virtual DbSet<AnimeSeriesEntity> AnimeSeries { get; set; } = default!;

    public virtual DbSet<AnimeSeriesGenreEntity> AnimeSeriesGenres { get; set; } = default!;

    public virtual DbSet<GenreEntity> Genres { get; set; } = default!;

    public virtual DbSet<RateAnimeSeriesEntity> Rates { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.AddInterceptors(_entitySaveChangesInterceptor);
    }
}