using AniMedia.Application.Common.Interfaces;
using AniMedia.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace AniMedia.Application.Common.Services;

public class DirectoryService : IDirectoryService {
    private readonly BinaryFileSettings _settings;

    public DirectoryService(IOptions<BinaryFileSettings> settings) {
        _settings = settings.Value;
    }

    /// <inheritdoc />
    public string GetBaseDirectory() {
        return AppContext.BaseDirectory;
    }

    /// <inheritdoc />
    public string GetBinaryFilesDirectory() {
        // Todo:
        //     Add handle incorrect config

        var dir = _settings.UseRootDirectory ? Path.Combine(GetBaseDirectory(), _settings.Directory) : _settings.Directory;

        if (Directory.Exists(dir) == false) {
            Directory.CreateDirectory(dir);
        }

        return dir;
    }

    /// <inheritdoc />
    public string GetNewRandomPathBinaryFile(string extension) {
        var dir = GetBinaryFilesDirectory();

        var randomName = GenerateRandomName() + (extension.StartsWith('.') ? extension : '.' + extension);

        var fullPath = Path.Combine(dir, randomName);

        if (Path.Exists(fullPath)) {
            // Todo:
            //     Recursion warning

            fullPath = GetNewRandomPathBinaryFile(extension);
        }

        return fullPath;
    }

    private string GenerateRandomName() {
        int lengthGuid = Guid.Empty.ToString("N").Length;

        var cycle = (int)Math.Floor((float)_settings.MaxLengthName / (float)lengthGuid);

        var str = string.Empty;

        for (int i = 0; i < cycle; i++) {
            str += Guid.NewGuid().ToString("N");
        }

        return str.Substring(0, _settings.MaxLengthName);
    }
}