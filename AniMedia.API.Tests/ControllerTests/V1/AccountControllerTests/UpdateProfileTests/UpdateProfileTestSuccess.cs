using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AccountControllerTests.UpdateProfileTests; 

public class UpdateProfileTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var newFName = CommandHelper.GetRandomString();
        var newSName = CommandHelper.GetRandomString();

        var res = await apiClient.ApiV1AccountUpdateAsync(newFName, newSName);

        res.Should().NotBeNull();
        res.UID.Should().Be(user.Profile.UID);
        res.FirstName.Should().Be(newFName);
        res.SecondName.Should().Be(newSName);
    }
}