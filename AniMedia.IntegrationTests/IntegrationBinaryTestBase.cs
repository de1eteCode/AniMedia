using AniMedia.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.IntegrationTests;

public abstract class IntegrationBinaryTestBase : IntegrationTestBase, IDisposable {

    public void Dispose() {
        var dirService = ServiceProvider.GetRequiredService<IDirectoryService>();

        var pathContent = dirService.GetBinaryFilesDirectory();

        foreach (var filePath in Directory.GetFiles(pathContent, "*.*")) {
            var fInfo = new FileInfo(filePath);

            if (fInfo.Exists) {
                fInfo.Delete();
            }
        }
    }

    protected byte[] GetRandomData(int length) {
        return Enumerable.Repeat(length, length)
            .Select(_ => (byte)Random.Shared.Next(255))
            .ToArray();
    }
}