using AniMedia.API.Tests.Helpers;
using AniMedia.API.Tests.HttpClients;
using AniMedia.Domain.Models.Responses;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AuthControllerTests;

public class DoubleRegistrationThrowAuthorization : ApiTestBase {
    
    [Fact]
    public override async Task Test() {
        var nick = CommandHelper.GetRandomString();
        var password = CommandHelper.GetRandomString(8);

        (var apiClient, var httpClient) = GetClient();
        
        var resp  = await apiClient.AuthRegistrationAsync(nick, password);
        
        await Assert.ThrowsAsync<ApiClientException<RegistrationError>>(async () => 
            await apiClient.AuthRegistrationAsync(nick, password));
    }
}