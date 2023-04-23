using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.GenreControllerTests.GetGenreTests; 

public class GetGenreTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var (apiClient, _) = GetClient();

        var genre = await this.CreateGenre();

        var res = await apiClient.ApiV1GenreIdAsync(genre.Uid);

        res.Should().NotBeNull();
        res.Uid.Should().Be(genre.Uid);
        res.Name.Should().Be(genre.Name);
    }
}