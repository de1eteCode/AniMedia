using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos; 

public class RateAnimeSeriesDto {

    public RateAnimeSeriesDto() {
        
    }

    public RateAnimeSeriesDto(RateAnimeSeriesEntity entity) {
        Uid = entity.UID;
        User = new ProfileUserDto(entity.User);
        AnimeSeries = new AnimeSeriesDto(entity.AnimeSeries);
        Rate = entity.Rate;
    }
    
    public Guid Uid { get; set; }

    public ProfileUserDto User { get; set; }

    public AnimeSeriesDto AnimeSeries { get; set; }

    public byte Rate { get; set; }
}