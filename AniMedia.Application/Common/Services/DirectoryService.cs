using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace AniMedia.Application.Common.Services;

public class DirectoryService : IDirectoryService {
    private readonly BinaryFileSettings _settings;

    public DirectoryService(IOptions<BinaryFileSettings> settings) {
        _settings = settings.Value;
    }

    /// <inheritdoc/>
    public string GetBaseDirectory() {
        return AppContext.BaseDirectory;
    }

    /// <inheritdoc/>
    public string GetBinaryFilesDirectory() {
        /// Todo:
        ///     Add handle incorrect config

        string dir = string.Empty;

        if (_settings.UseRootDirectory) {
            dir = Path.Combine(GetBaseDirectory(), _settings.Directory);
        }
        else {
            dir = _settings.Directory;
        }

        if (Directory.Exists(dir) == false) {
            Directory.CreateDirectory(dir);
        }

        return dir;
    }

    /// <inheritdoc/>
    public string GetNewRandomPathBinaryFile() {
        var dir = GetBinaryFilesDirectory();

        var randomName = Guid.NewGuid().ToString("N") + Path.GetRandomFileName();

        var fullPath = Path.Combine(dir, randomName);

        if (Path.Exists(fullPath)) {
            /// Todo:
            ///     Recursion warning

            fullPath = GetNewRandomPathBinaryFile();
        }

        return fullPath;
    }
}