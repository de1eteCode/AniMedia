using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AccountControllerTests.UpdateAvatarTests; 

public class UpdateAvatarTestSuccess : ApiTestBase{

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);
        
        await using var fParamBuilder = new MockFileBuilder();
        var fParam = await fParamBuilder
            .SetContentSize(32)
            .SetContentType("image/jpg")
            .SetFileExtension("jpg")
            .Build();
        
        var res = await apiClient.ApiV1AccountUpdateavatarAsync(fParam);

        res.Should().NotBeNull();
        res.UID.Should().NotBeEmpty();
        res.Name.Should().NotBeEmpty();
        res.ContentType.Should().Be(fParamBuilder.ContentType);
    }
}