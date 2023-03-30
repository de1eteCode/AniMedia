using AniMedia.Application.ApiCommands.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Auth.LoginCommandHandlerTests;

public class LoginTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        await RequestAsync(CommandHelper.RegistrationDe1ete());

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationDe1ete().Nickname,
            CommandHelper.RegistrationDe1ete().Password,
            "216.28.34.2",
            "Google Chrome 111");

        var loginResult = await RequestAsync(loginCommand);

        loginResult.IsSuccess.Should().BeTrue();
    }
}