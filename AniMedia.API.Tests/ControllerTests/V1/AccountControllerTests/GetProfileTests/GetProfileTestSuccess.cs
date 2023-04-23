using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AccountControllerTests.GetProfileTests; 

public class GetProfileTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var res = await apiClient.ApiV1AccountProfileAsync();

        res.Should().NotBeNull();
        res.UID.Should().Be(user.Profile.UID);
        res.NickName.Should().Be(user.Profile.NickName);
        res.FirstName.Should().Be(user.Profile.FirstName);
        res.SecondName.Should().Be(user.Profile.SecondName);
    }
}