using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.MediaControllerTests.GetInfoTests; 

public class GetInfoFileTestSuccess : ApiTestBase{

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        var contentType = "binary/*";
        var file = await this.CreateFile(contentType, "bin");

        var resByUid = await apiClient.ApiV1MediaInfoAsync(file.UID.ToString());
        var resByName = await apiClient.ApiV1MediaInfoAsync(file.Name);

        resByUid.Should().NotBeNull();
        resByUid.UID.Should().Be(file.UID);
        resByUid.Name.Should().Be(file.Name);
        resByUid.ContentType.Should().Be(contentType);
        
        resByName.Should().NotBeNull();
        resByName.UID.Should().Be(file.UID);
        resByName.Name.Should().Be(file.Name);
        resByName.ContentType.Should().Be(contentType);
    }
}