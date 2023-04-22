using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests.LoginTests;

public class LoginTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetRegisteredUser();
        var (apiClient, _) = GetClient();

        var req = await apiClient.AuthLoginAsync(user.Profile.NickName, user.Password);
        
        req.Should().NotBeNull();
        req.AccessToken.Should().NotBeEmpty();
        req.RefreshToken.Should().NotBeEmpty();
    }
}