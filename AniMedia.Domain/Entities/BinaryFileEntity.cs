using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

public class BinaryFileEntity : IBaseEntity {
    public Guid UID { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = default!;

    public string ContentType { get; set; } = default!;

    public long Length { get; set; }

    public string Hash { get; set; } = default!;

    public BinaryFileEntity(string name, string contentType, long length, string hash) {
        Name = name;
        ContentType = contentType;
        Length = length;
        Hash = hash;
    }
}