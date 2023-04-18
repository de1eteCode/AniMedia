using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Responses;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.Auth.GetSessionListQueryHandlerTests;

public class GetSessionListTestThrowAuthentication : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var getSessionsQuery = new GetSessionListQueryCommand(0, 0);

        var result = await RequestAsync(getSessionsQuery);

        result.Error.Should().BeOfType<AuthenticationError>();
    }
}