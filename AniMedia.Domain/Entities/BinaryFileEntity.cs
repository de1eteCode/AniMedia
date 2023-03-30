using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

public class BinaryFileEntity : IBaseEntity {

    public BinaryFileEntity(string name, string pathFile, string contentType, long length, string hash) {
        Name = name;
        PathFile = pathFile;
        ContentType = contentType;
        Length = length;
        Hash = hash;
    }

    public string ContentType { get; set; } = default!;

    public string Hash { get; set; } = default!;

    public long Length { get; set; }

    public string Name { get; set; } = default!;

    public string PathFile { get; set; } = default!;

    public Guid UID { get; set; } = Guid.NewGuid();
}