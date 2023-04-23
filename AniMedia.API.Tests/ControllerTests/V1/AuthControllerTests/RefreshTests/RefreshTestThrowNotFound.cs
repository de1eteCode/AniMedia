using AniMedia.API.Tests.HttpClients;
using AniMedia.Domain.Models.Responses;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests.RefreshTests;

public class RefreshTestThrowNotFound : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        await Assert.ThrowsAsync<ApiClientException<EntityNotFoundError>>(async () =>
            await apiClient.AuthRefreshAsync(Guid.NewGuid()));
    }
}