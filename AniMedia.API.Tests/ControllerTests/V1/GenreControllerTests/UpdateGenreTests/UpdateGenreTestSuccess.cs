using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.GenreControllerTests.UpdateGenreTests; 

public class UpdateGenreTestSuccess : ApiTestBase{

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        var genre = await this.CreateGenre();
        httpClient.SetAuthorizationToken(user.AccessToken);

        var newNameGenre = CommandHelper.GetRandomString();

        var res = await apiClient.ApiV1GenrePutAsync(genre.Uid, newNameGenre);

        res.Should().NotBeNull();
        res.Uid.Should().Be(genre.Uid);
        res.Name.Should().NotBe(genre.Name);
        res.Name.Should().Be(newNameGenre);
    }
}