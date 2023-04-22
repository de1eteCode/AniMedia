using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Responses;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.Auth.GetSessionListQueryHandlerTests;

public class GetSessionListTestThrowNotFound : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var getSessionsQuery = new GetSessionListQueryCommand(Guid.NewGuid(), 1, 100);

        var result = await RequestAsync(getSessionsQuery);

        result.Error.Should().BeOfType<EntityNotFoundError>();
    }
}