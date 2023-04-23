using AniMedia.API.Tests.HttpClients;

namespace AniMedia.API.Tests.Helpers; 

public class MockFileBuilder : IDisposable, IAsyncDisposable {

    private readonly MemoryStream _memoryStream;

    public int FileByteSize { get; private set; } = 32;

    public string ContentType { get; private set; } = "binary";
    
    public string FileExtension { get; private set; } = "bin";

    public MockFileBuilder() {
        _memoryStream = new MemoryStream();
    }
    
    public MockFileBuilder SetContentSize(int size) {
        if (size < 1) {
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        FileByteSize = size;

        return this;
    }
    
    public MockFileBuilder SetContentType(string contentType) {
        ArgumentException.ThrowIfNullOrEmpty(contentType);
        
        ContentType = contentType;

        return this;
    }

    public MockFileBuilder SetFileExtension(string extension) {
        ArgumentException.ThrowIfNullOrEmpty(extension);

        if (extension.StartsWith('.')) {
            extension = extension.Substring(1);
        }
        
        ArgumentException.ThrowIfNullOrEmpty(extension);

        FileExtension = extension;
        
        return this;
    }

    public async Task<FileParameter> Build() {
        await _memoryStream.WriteAsync(GenerateContent(FileByteSize).ToArray());
        
        _memoryStream.Seek(0, SeekOrigin.Begin);
        
        return new FileParameter(
            _memoryStream, 
            $"{Guid.NewGuid().ToString("N")}.{FileExtension}",
            ContentType);
    }

    private static IEnumerable<byte> GenerateContent(int size) {
        for (var i = 0; i < size; i++) {
            yield return (byte)Random.Shared.Next(0, byte.MaxValue);
        }
    }

    public void Dispose() {
        _memoryStream.Dispose();
    }

    public ValueTask DisposeAsync() {
        return _memoryStream.DisposeAsync();
    }
}