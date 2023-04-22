using AniMedia.API.Tests.Helpers;
using AniMedia.API.Tests.HttpClients;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AccountControllerTests.UpdateAvatarTests; 

public class UpdateAvatarTestSuccess : ApiTestBase{

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var content = CommandHelper.GetRandomString(15, 50);
        var contentType = "image/jpg";
        var fileName = CommandHelper.GetRandomString() + ".jpg";

        using var mStream = new MemoryStream();
        await using var writer = new StreamWriter(mStream);

        await writer.WriteAsync(content);
        await writer.FlushAsync();

        mStream.Seek(0, SeekOrigin.Begin);

        var fParam = new FileParameter(mStream, fileName, contentType);
        
        var res = await apiClient.ApiV1AccountUpdateavatarAsync(fParam);

        res.Should().NotBeNull();
        res.UID.Should().NotBeEmpty();
        res.Name.Should().NotBeEmpty();
        res.ContentType.Should().Be(contentType);
    }
}