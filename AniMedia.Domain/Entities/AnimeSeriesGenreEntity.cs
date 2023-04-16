using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Связующая сущность аниме сериала и жанра
/// </summary>
public class AnimeSeriesGenreEntity : BaseAuditableEntity {
    
    /// <summary>
    /// Идентификатор жанра
    /// </summary>
    public Guid GenreUid { get; set; }
    
    /// <summary>
    /// Идентификатор аниме сериала
    /// </summary>
    public Guid AnimeSeriesUid { get; set; }

    /// <summary>
    /// Жанр
    /// </summary>
    public virtual GenreEntity Genre { get; set; } = default!;
    
    /// <summary>
    /// Аниме сериал
    /// </summary>
    public virtual AnimeSeriesEntity AnimeSeries { get; set; } = default!;
}