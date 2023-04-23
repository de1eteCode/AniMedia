using AniMedia.API.Tests.Helpers;
using AniMedia.API.Tests.HttpClients;
using AniMedia.Domain.Models.Responses;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AccountControllerTests.GetProfileTests;

public class GetProfileThrowAuthentication : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, httpClient) = GetClient();
        httpClient.SetAuthorizationToken("invalid_token");

        await Assert.ThrowsAsync<ApiClientException<AuthenticationError>>(async () =>
            await apiClient.ApiV1AccountProfileAsync());
    }
}