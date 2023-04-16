using AniMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AniMedia.Application.Common.Interfaces;

public interface IApplicationDbContext {

    /// <summary>
    /// Бинарные файлы
    /// </summary>
    public DbSet<BinaryFileEntity> BinaryFiles { get; }

    /// <summary>
    /// Сессии пользователей
    /// </summary>
    public DbSet<SessionEntity> Sessions { get; }

    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<UserEntity> Users { get; }
    
    /// <summary>
    /// Аниме сериалы
    /// </summary>
    public DbSet<AnimeSeriesEntity> AnimeSeries { get; }
    
    /// <summary>
    /// Связка жанров и аниме сериалов
    /// </summary>
    public DbSet<AnimeSeriesGenreEntity> AnimeSeriesGenres { get; }
    
    /// <summary>
    /// Жанры
    /// </summary>
    public DbSet<GenreEntity> Genres { get; }
    
    /// <summary>
    /// Оценки аниме сериалов
    /// </summary>
    public DbSet<RateAnimeSeriesEntity> Rates { get; }

    /// <summary>
    /// Сохранение данных
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество затронутых строк при обновлении базы данных</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}