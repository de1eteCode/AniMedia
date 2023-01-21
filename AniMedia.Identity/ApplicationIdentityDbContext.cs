using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Identity;

internal class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid> {

    public ApplicationIdentityDbContext(DbContextOptions options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}