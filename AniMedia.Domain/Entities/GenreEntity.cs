using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities; 

/// <summary>
/// Жанр
/// </summary>
public class GenreEntity : BaseAuditableEntity {

    public GenreEntity(string name) {
        Name = name;
        AnimeSeries = new List<AnimeSeriesGenreEntity>();
    }
    
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } 
    
    /// <summary>
    /// Аниме сериалы жанра
    /// </summary>
    public virtual ICollection<AnimeSeriesGenreEntity> AnimeSeries { get; set; }
}