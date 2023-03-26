using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class BinaryFileDto {
    public Guid UID { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string ContentType { get; set; } = default!;

    public BinaryFileDto() {
    }

    public BinaryFileDto(BinaryFileEntity binaryFile) {
        UID = binaryFile.UID;
        Name = binaryFile.Name;
        ContentType = binaryFile.ContentType;
    }
}