using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AnimeSeriesControllerTests.GetAnimeSeriesTests; 

public class GetAnimeSeriesListTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        for (var i = 0; i < 10; i++) {
            await this.CreateAnimeSeries();
        }

        var res = await apiClient.ApiV1AnimeSeriesGetAsync(1, 100);

        res.Should().NotBeNull();
        res.Items.Count().Should().Be(10);
    }
}