using AniMedia.Application.ApiCommands.Genres;
using AniMedia.Domain.Models.Responses;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Genres.AddGenreCommandHandlerTests;

public class AddGenreTestThrowAuthentication : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        // request
        var rndStr = CommandHelper.GetRandomString();
        var request = new AddGenreCommand(rndStr);
        var result = await RequestAsync(request);

        // assert
        result.Error.Should().BeOfType<AuthenticationError>();
    }
}