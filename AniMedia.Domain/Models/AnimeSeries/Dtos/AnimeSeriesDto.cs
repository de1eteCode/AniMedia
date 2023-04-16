using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.AnimeSeries.Dtos; 

public class AnimeSeriesDto {

    public AnimeSeriesDto() {
        
    }

    public AnimeSeriesDto(AnimeSeriesEntity entity) {
        Uid = entity.UID;
        Name = entity.Name;
        JapaneseName = entity.JapaneseName;
        Description = entity.Description;
        DateOfRelease = entity.DateOfRelease;
        DateOfAnnouncement = entity.DateOfAnnouncement;
        ExistTotalEpisodes = entity.ExistTotalEpisodes;
        PlanedTotalEpisodes = entity.PlanedTotalEpisodes;
    }

    public Guid Uid { get; set; }
    
    public string Name { get; set; }
    
    public string? JapaneseName { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? DateOfRelease { get; set; }
    
    public DateTime? DateOfAnnouncement { get; set; }
    
    public int? ExistTotalEpisodes { get; set; }
    
    public int? PlanedTotalEpisodes { get; set; }
}