using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.GenreControllerTests.GetGenreTests; 

public class GetGenresListTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        for (int i = 0; i < 10; i++) {
            await this.CreateGenre();
        }

        var res = await apiClient.ApiV1GenreGetAsync(1, 100);

        res.Should().NotBeNull();
        res.Items.Count().Should().Be(10);

        foreach (var item in res.Items) {
            item.Uid.Should().NotBeEmpty();
        }
    }
}