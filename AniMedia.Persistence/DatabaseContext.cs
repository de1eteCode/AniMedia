using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AniMedia.Persistence;

public class DatabaseContext : DbContext, IApplicationDbContext {

    public DatabaseContext(DbContextOptions options) : base(options) {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}