using AniMedia.Application.ApiCommands.Binary;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Binary.RemoveBinaryFileCommandHandlerTests;

public class RemoveBinaryFIleTestSuccess : IntegrationBinaryTestBase {

    [Fact]
    public override async Task Test() {
        /// create file
        var data = GetRandomData(128);
        using var memStream = new MemoryStream();
        await memStream.WriteAsync(data, 0, data.Length);
        memStream.Position = 0;
        var hashData = await GetHashStream(memStream);
        memStream.Position = 0;

        /// save file
        var commandSave = new SaveBinaryFileCommand(memStream, nameof(data), "binary bytes", hashData);
        var resSaveFile = await RequestAsync(commandSave);

        /// delete file
        var commandRemove = new RemoveBinaryFileCommand(resSaveFile.Value!.UID);
        var resRemove = await RequestAsync(commandRemove);

        /// assert
        resRemove.IsSuccess.Should().BeTrue();
        resRemove.Value!.Should().NotBe(resSaveFile.Value!);
        resRemove.Value!.UID.Should().Be(resSaveFile.Value!.UID);
        resRemove.Value!.ContentType.Should().Be(resSaveFile.Value!.ContentType);
    }
}