using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Responses;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.GetSessionQueryHandlerTests;

public class GetSessionThrowAuthentication : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var getSessionsQuery = new GetSessionQueryCommand("wrong_access_token");

        var result = await RequestAsync(getSessionsQuery);

        result.Error.Should().BeOfType<AuthenticationError>();
    }
}