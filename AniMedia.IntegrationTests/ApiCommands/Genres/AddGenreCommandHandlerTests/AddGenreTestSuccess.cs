using AniMedia.Application.ApiCommands.Genres;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Genres.AddGenreCommandHandlerTests;

public class AddGenreTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        SetUser((await RequestAsync(CommandHelper.RegistrationCommandDe1ete())).Value!.UID);

        // request
        var rndStr = CommandHelper.GetRandomString();
        var request = new AddGenreCommand(rndStr);
        var result = await RequestAsync(request);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Uid.Should().NotBeEmpty();
        result.Value!.Name.Should().Be(rndStr);
    }
}