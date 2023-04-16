namespace AniMedia.Domain.Models.AnimeSeries.Requests; 

public record UpdateAnimeSeriesRequest(Guid Uid) : AddAnimeSeriesRequest;