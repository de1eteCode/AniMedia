using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class BinaryFileDto
{

    public BinaryFileDto()
    {
    }

    public BinaryFileDto(BinaryFileEntity binaryFile)
    {
        ArgumentNullException.ThrowIfNull(binaryFile);

        UID = binaryFile.UID;
        Name = binaryFile.Name;
        ContentType = binaryFile.ContentType;
    }

    public string ContentType { get; set; } = default!;

    public string Name { get; set; } = default!;

    public Guid UID { get; set; }
}