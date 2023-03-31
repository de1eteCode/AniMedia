using System.Reflection;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Persistence;

public class DatabaseContext : DbContext, IApplicationDbContext {

    public DatabaseContext(DbContextOptions options) : base(options) {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public virtual DbSet<BinaryFileEntity> BinaryFiles { get; set; }

    public virtual DbSet<SessionEntity> Sessions { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}