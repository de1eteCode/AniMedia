using AniMedia.API.Tests.Helpers;
using AniMedia.API.Tests.HttpClients;
using AniMedia.Domain.Models.Responses;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests.LoginTests;

public class LoginThrowAuthorization : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        await Assert.ThrowsAsync<ApiClientException<AuthorizationError>>(async () =>
            await apiClient.AuthLoginAsync(CommandHelper.GetRandomString(), CommandHelper.GetRandomString()));
    }
}