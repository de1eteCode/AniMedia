using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Аниме сериал
/// </summary>
public class AnimeSeriesEntity : BaseAuditableEntity {

    /// <summary>
    /// Наименование аниме сериала
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Наименование аниме сериала на английском
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// Наименование аниме сериала на японском
    /// </summary>
    public string JapaneseName { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата релиза аниме сериала
    /// </summary>
    public DateTime? DateOfRelease { get; set; }

    /// <summary>
    /// Дата анонсирования аниме сериала
    /// </summary>
    public DateTime? DateOfAnnouncement { get; set; }

    /// <summary>
    /// Всего выпущено эпизодов
    /// </summary>
    public int? ExistTotalEpisodes { get; set; }

    /// <summary>
    /// Запланированое количество эпизодов
    /// </summary>
    public int? PlanedTotalEpisodes { get; set; }

    /// <summary>
    /// Жанры аниме сериала
    /// </summary>
    public virtual ICollection<AnimeSeriesGenreEntity> Genres { get; set; } = new List<AnimeSeriesGenreEntity>();

    /// <summary>
    /// Оценки аниме сериала от пользователей
    /// </summary>
    public virtual ICollection<RateAnimeSeriesEntity> Rates { get; set; } = new List<RateAnimeSeriesEntity>();
}