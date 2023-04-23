using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.SessionControllerTests.GetSessionTests;

public class GetSessionTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var session = await apiClient.ApiV1SessionAsync(user.AccessToken);

        session.Should().NotBeNull();
        session.Uid.Should().NotBeEmpty();
    }
}