using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests.RegistrationTests; 

public class RegistrationTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var nick = CommandHelper.GetRandomString();
        var password = CommandHelper.GetRandomString(8);

        (var apiClient, var httpClient) = GetClient();
        
        httpClient.SetUserAgent();
        httpClient.SetRemoteIp();
        
        var response = await apiClient.AuthRegistrationAsync(nick, password);

        response.Should().NotBeNull();
        response.AccessToken.Should().NotBeNull();
        response.RefreshToken.Should().NotBeEmpty();
    }
}