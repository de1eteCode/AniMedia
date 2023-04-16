using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities; 

/// <summary>
/// Жанр
/// </summary>
public class GenreEntity : BaseAuditableEntity {

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Аниме сериалы жанра
    /// </summary>
    public virtual ICollection<AnimeSeriesGenreEntity> AnimeSeries { get; set; }
}