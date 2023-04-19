using AniMedia.Application.ApiCommands.Account;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Account.UpdateAvatarCommandHandlerTests;

public class UpdateAvatarTestSuccess : IntegrationBinaryTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationCommandDe1ete());
        SetUser(de1eteUser.Value!.UID);

        // request
        var rndBin = GetRandomData(Random.Shared.Next(2, 512));
        var contentType = "image/*";
        using var ms = new MemoryStream();
        await ms.WriteAsync(rndBin, 0, rndBin.Length);
        ms.Position = 0;

        var request = new UpdateAvatarCommand(de1eteUser.Value!.UID, ms, contentType);

        var result = await RequestAsync(request);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.UID.Should().NotBeEmpty();
        result.Value!.Name.Should().NotBeEmpty();
        result.Value!.ContentType.Should().Be(contentType);
    }
}