using AniMedia.API.Tests.Helpers;
using AniMedia.API.Tests.HttpClients;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.MediaControllerTests.GetFileTests;

public class GetFileTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        var contentType = "binary/*";
        var file = await this.CreateFile(contentType, "bin");

        var resByName = await apiClient.ApiV1MediaFileAsync(file.Name);
        var resByUid = await apiClient.ApiV1MediaFileAsync(file.UID.ToString());

        resByName.Should().NotBeNull();
        resByUid.Should().NotBeNull();

        GetContentType(resByName).Should().Be(contentType);
        GetContentType(resByUid).Should().Be(contentType);

        GetName(resByName).Should().Be(file.Name);
        GetName(resByUid).Should().Be(file.Name);
    }

    private static string GetContentType(FileResponse fileResponse) {
        if (fileResponse.Headers.TryGetValue("Content-Type", out var listTypes)) {
            return listTypes.First();
        }
        
        return string.Empty;
    }

    private static string GetName(FileResponse fileResponse) {
        if (fileResponse.Headers.TryGetValue("Content-Disposition", out var list)) {
            var split = list.First().Split(';');

            var fileName = split[1].Substring(10); // "filename=...."

            return fileName;
        }
        
        return string.Empty;
    }
}