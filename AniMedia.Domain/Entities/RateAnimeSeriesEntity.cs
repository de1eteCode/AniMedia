using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Оценка аниме серала от пользователя
/// </summary>
public class RateAnimeSeriesEntity : BaseAuditableEntity {
    
    /// <summary>
    /// Оценка
    /// </summary>
    public byte Rate { get; set; }
    
    /// <summary>
    /// Идентификатор аниме сериала
    /// </summary>
    public Guid AnimeSeriesUid { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserUid { get; set; }
    
    /// <summary>
    /// Аниме сериал
    /// </summary>
    public virtual AnimeSeriesEntity AnimeSeries { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual UserEntity User { get; set; }
}