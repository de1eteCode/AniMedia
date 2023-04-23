using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class AnimeSeriesDto {

    public AnimeSeriesDto() {
        Genres = new List<GenreDto>();
    }

    public AnimeSeriesDto(AnimeSeriesEntity entity) {
        Uid = entity.UID;
        Name = entity.Name;
        EnglishName = entity.EnglishName;
        JapaneseName = entity.JapaneseName;
        Description = entity.Description;
        DateOfRelease = entity.DateOfRelease;
        DateOfAnnouncement = entity.DateOfAnnouncement;
        ExistTotalEpisodes = entity.ExistTotalEpisodes;
        PlanedTotalEpisodes = entity.PlanedTotalEpisodes;
        Genres = entity.Genres
            .Select(e => new GenreDto(e.Genre));
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string EnglishName { get; set; }

    public string JapaneseName { get; set; }

    public string Description { get; set; }

    public DateTime? DateOfRelease { get; set; }

    public DateTime? DateOfAnnouncement { get; set; }

    public int? ExistTotalEpisodes { get; set; }

    public int? PlanedTotalEpisodes { get; set; }

    public IEnumerable<GenreDto> Genres { get; set; }
}