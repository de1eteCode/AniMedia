namespace AniMedia.Application.Common.Models;

public class BinaryFileSettings {
    public bool UseRootDirectory { get; set; }

    public string Directory { get; set; } = default!;
}