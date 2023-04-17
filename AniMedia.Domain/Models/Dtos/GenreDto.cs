using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class GenreDto
{

    public GenreDto()
    {
    }

    public GenreDto(GenreEntity entity)
    {
        Uid = entity.UID;
        Name = entity.Name;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

}