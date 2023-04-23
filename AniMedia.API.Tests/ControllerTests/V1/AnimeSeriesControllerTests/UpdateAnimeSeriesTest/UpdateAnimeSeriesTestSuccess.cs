using AniMedia.API.Tests.Helpers;
using AniMedia.Domain.Models.Dtos;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.AnimeSeriesControllerTests.UpdateAnimeSeriesTest; 

public class UpdateAnimeSeriesTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        var animeSeries = await this.CreateAnimeSeries();
        httpClient.SetAuthorizationToken(user.AccessToken);

        var genres = new List<GenreDto>();

        for (var i = 0; i < 5; i++) {
            genres.Add(await this.CreateGenre());
        }
        
        var announcement = Random.Shared.Next(600, 6000);
        var newAnimeSeriesDto = new AnimeSeriesDto {
            Uid = animeSeries.Uid,
            Name = CommandHelper.GetRandomString(),
            EnglishName = CommandHelper.GetRandomString(),
            JapaneseName = CommandHelper.GetRandomString(),
            Description = CommandHelper.GetRandomString(20, 100),
            Genres = genres,
            DateOfAnnouncement = DateTime.Now.AddDays(-1 * announcement),
            DateOfRelease = DateTime.Now.AddDays(-1 * Random.Shared.Next(1, announcement)),
            ExistTotalEpisodes = Random.Shared.Next(100, 300),
            PlanedTotalEpisodes = Random.Shared.Next(300, 1500)
        };

        var res = await apiClient.ApiV1AnimeSeriesUpdateAsync(animeSeries.Uid, newAnimeSeriesDto);

        res.Should().NotBeNull();
        res.Uid.Should().Be(animeSeries.Uid);
        res.Name.Should().Be(newAnimeSeriesDto.Name);
        res.EnglishName.Should().Be(newAnimeSeriesDto.EnglishName);
        res.JapaneseName.Should().Be(newAnimeSeriesDto.JapaneseName);
        res.Description.Should().Be(newAnimeSeriesDto.Description);
        res.ExistTotalEpisodes.Should().Be(newAnimeSeriesDto.ExistTotalEpisodes);
        res.PlanedTotalEpisodes.Should().Be(newAnimeSeriesDto.PlanedTotalEpisodes);
        res.Genres.Count().Should().Be(newAnimeSeriesDto.Genres.Count());
    }
}