using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Abstracts;
using AniMedia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AniMedia.Persistence.Interceptors; 

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor {

    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public AuditableEntitySaveChangesInterceptor(
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService) {
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) {
        UpdateEntities(eventData.Context);
        
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken()) {
        UpdateEntities(eventData.Context);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context) {
        if (context is null) {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>()) {
            if (entry.State == EntityState.Added) {
                entry.Entity.CreateByUid = _currentUserService.UserUID;
                entry.Entity.CreateAt = _dateTimeService.Now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities()) {
                entry.Entity.LastModifiedByUid = _currentUserService.UserUID;
                entry.Entity.LastModified = _dateTimeService.Now;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}