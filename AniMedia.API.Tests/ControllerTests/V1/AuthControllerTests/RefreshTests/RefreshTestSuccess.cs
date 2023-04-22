using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests.RefreshTests; 

public class RefreshTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser ();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var resp = await apiClient.AuthRefreshAsync(user.RefreshToken);

        resp.Should().NotBeNull();
        resp.UID.Should().Be(user.Profile.UID);
        resp.AccessToken.Should().NotBeEmpty(); 
        resp.RefreshToken.Should().NotBeEmpty();
        resp.AccessToken.Should().NotBe(user.AccessToken);
        resp.RefreshToken.Should().NotBe(user.RefreshToken);
    }
}