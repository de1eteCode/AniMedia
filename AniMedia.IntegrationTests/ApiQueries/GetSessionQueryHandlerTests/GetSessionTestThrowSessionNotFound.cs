using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Responses;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.GetSessionQueryHandlerTests;

public class GetSessionTestThrowSessionNotFound : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationDe1ete());

        SetUser(de1eteUser.Value!.UID);

        var getSessionQuery = new GetSessionQueryCommand("wrong_access_token");

        var result = await RequestAsync(getSessionQuery);

        result.Error.Should().BeOfType<EntityNotFoundError>();
    }
}