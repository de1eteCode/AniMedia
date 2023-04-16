using AniMedia.Domain.Models.Genres.Dtos;

namespace AniMedia.Domain.Models.AnimeSeries.Requests;

public record AddAnimeSeriesRequest {
    public required string Name { get; init; }
    public required string JapaneseName { get; init; }
    public required string Description { get; init; }
    public DateTime? DateOfRelease { get; init; }
    public DateTime? DateOfAnnouncement { get; init; }
    public int? ExistTotalEpisodes { get; init; }
    public int? PlanedTotalEpisodes { get; init; }

    public ICollection<GenreDto> Genres { get; init; } = new List<GenreDto>();
};