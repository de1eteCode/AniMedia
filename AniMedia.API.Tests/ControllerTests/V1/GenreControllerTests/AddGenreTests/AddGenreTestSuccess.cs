using AniMedia.API.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.API.Tests.ControllerTests.V1.GenreControllerTests.AddGenreTests; 

public class AddGenreTestSuccess : ApiTestBase {

    [Fact]
    public override async Task Test() {
        var user = await this.GetLoggedRegisteredUser();
        var (apiClient, httpClient) = GetClient();
        
        httpClient.SetAuthorizationToken(user.AccessToken);

        var nameGenre = CommandHelper.GetRandomString();

        var res = await apiClient.ApiV1GenrePostAsync(nameGenre);

        res.Should().NotBeNull();
        res.Uid.Should().NotBeEmpty();
        res.Name.Should().Be(nameGenre);
    }
}