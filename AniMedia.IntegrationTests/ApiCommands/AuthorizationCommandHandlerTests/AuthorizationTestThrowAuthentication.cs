using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Domain.Models.Responses;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.AuthorizationCommandHandlerTests;

public class AuthorizationTestThrowAuthentication : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var authCommand = new AuthorizationCommand("random_access_token");

        var authResult = await RequestAsync(authCommand);

        authResult.Error.Should().BeOfType<AuthenticationError>();
    }
}