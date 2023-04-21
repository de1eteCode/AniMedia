using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AnimeSeriesControllerTests; 

public class RegistrationTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var nick = "SomeNick";
        var password = "P@ssw0rd";
        
        var response = await _apiClient.AuthRegistrationAsync(nick, password);

        response.Should().NotBeNull();
        response.AccessToken.Should().NotBeNull();
        response.RefreshToken.Should().NotBeEmpty();
    }
}