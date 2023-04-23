using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.SessionControllerTests.GetSessionTests; 

public class GetSessionListTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);
        
        var sessionListRes = await apiClient.ApiV1SessionListAsync(1, 100);

        sessionListRes.Should().NotBeNull();
        sessionListRes.Items.Count().Should().Be(1);
    }
}