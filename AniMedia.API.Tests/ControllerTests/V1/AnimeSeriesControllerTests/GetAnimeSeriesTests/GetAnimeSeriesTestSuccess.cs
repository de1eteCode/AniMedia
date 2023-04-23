using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AnimeSeriesControllerTests.GetAnimeSeriesTests; 

public class GetAnimeSeriesTestSuccess : ApiTestBase{

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        var animeSeries = await this.CreateAnimeSeries();

        var res = await apiClient.ApiV1AnimeSeriesGetAsync(animeSeries.Uid);

        res.Should().NotBeNull();
        res.Uid.Should().Be(animeSeries.Uid);
        res.Name.Should().Be(animeSeries.Name);
        res.EnglishName.Should().Be(animeSeries.EnglishName);
        res.JapaneseName.Should().Be(animeSeries.JapaneseName);
        res.Description.Should().Be(animeSeries.Description);
        //res.DateOfRelease.Should().Be(animeSeries.DateOfRelease);
        //res.DateOfAnnouncement.Should().Be(animeSeries.DateOfAnnouncement);
        res.ExistTotalEpisodes.Should().Be(animeSeries.ExistTotalEpisodes);
        res.PlanedTotalEpisodes.Should().Be(animeSeries.PlanedTotalEpisodes);
        res.Genres.Count().Should().Be(animeSeries.Genres.Count());
    }
}