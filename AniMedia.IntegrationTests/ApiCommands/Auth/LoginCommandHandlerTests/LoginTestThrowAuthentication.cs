using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Domain.Models.Responses;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Auth.LoginCommandHandlerTests;

public class LoginTestThrowAuthentication : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        await RequestAsync(CommandHelper.RegistrationCommandDe1ete());

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationCommandDe1ete().Nickname,
            "wrong password",
            "226.28.34.2",
            "Google Chrome 111");

        var loginResult = await RequestAsync(loginCommand);

        loginResult.Error.Should().BeOfType<AuthorizationError>();
    }
}