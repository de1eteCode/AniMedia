using AniMedia.Application.ApiCommands.Binary;
using AniMedia.Domain.Models.Responses;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Binary.SaveBinaryFileCommandHandlerTests;

public class SaveBinaryTestThrowBinaryFileError : IntegrationBinaryTestBase {

    [Fact]
    public override async Task Test() {
        var data = GetRandomData(128);

        using var memStream = new MemoryStream();
        await memStream.WriteAsync(data, 0, data.Length);
        memStream.Position = 0;

        var commandSave = new SaveBinaryFileCommand(memStream, nameof(data), "binary bytes", "not equal to hash");

        var result = await RequestAsync(commandSave);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeOfType<BinaryFileError>();
    }
}