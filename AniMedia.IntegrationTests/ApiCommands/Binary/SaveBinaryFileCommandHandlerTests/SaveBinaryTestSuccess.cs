using AniMedia.Application.ApiCommands.Binary;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Binary.SaveBinaryFileCommandHandlerTests;

public class SaveBinaryTestSuccess : IntegrationBinaryTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        SetUser((await RequestAsync(CommandHelper.RegistrationCommandDe1ete())).Value!.UID);
        
        var data = GetRandomData(128);

        using var memStream = new MemoryStream();
        await memStream.WriteAsync(data, 0, data.Length);
        memStream.Position = 0;

        var commandSave = new SaveBinaryFileCommand(memStream,  "binary bytes");

        var result = await RequestAsync(commandSave);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Name.Should().NotBeEmpty();
        result.Value!.UID.Should().NotBe(Guid.Empty);
    }
}