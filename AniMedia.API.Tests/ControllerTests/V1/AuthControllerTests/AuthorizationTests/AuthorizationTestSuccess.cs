using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests.AuthorizationTests; 

public class AuthorizationTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, _) = GetClient();

        var res = await apiClient.AuthAuthorizationAsync(user.AccessToken);

        res.Should().NotBeNull();
        res.AccessToken.Should().NotBeEmpty();
        res.AccessToken.Should().NotBe(user.AccessToken);
        res.RefreshToken.Should().NotBeEmpty();
        res.RefreshToken.Should().Be(user.RefreshToken);
    }
}