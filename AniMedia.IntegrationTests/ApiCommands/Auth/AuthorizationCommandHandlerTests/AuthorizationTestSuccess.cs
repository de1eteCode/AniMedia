using AniMedia.Application.ApiCommands.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Auth.AuthorizationCommandHandlerTests;

public class AuthorizationTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationDe1ete());

        var authCommand = new AuthorizationCommand(de1eteUser.Value!.AccessToken);

        var authResult = await RequestAsync(authCommand);

        authResult.IsSuccess.Should().BeTrue();
    }
}