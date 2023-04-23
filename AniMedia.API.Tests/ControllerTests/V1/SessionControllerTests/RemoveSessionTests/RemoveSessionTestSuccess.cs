using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.SessionControllerTests.RemoveSessionTests; 

public class RemoveSessionTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var createdSession = await this.CreateSession(user);

        var res = await apiClient.ApiV1SessionRemoveAsync(createdSession.Uid);

        //var sessionListRes = await apiClient.ApiV1SessionListAsync(1, 100);

        res.Should().NotBeNull();
        res.Uid.Should().Be(createdSession.Uid);
        //sessionListRes.Count.Should().Be(1);
    }
}