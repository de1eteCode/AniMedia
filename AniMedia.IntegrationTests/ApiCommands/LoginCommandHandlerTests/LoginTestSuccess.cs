using AniMedia.Application.ApiCommands.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.LoginCommandHandlerTests;

public class LoginTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        await RequestAsync(CommandHelper.RegistrationDe1ete());

        var loginCommand = new LoginCommand(
            Nickname: CommandHelper.RegistrationDe1ete().Nickname,
            Password: CommandHelper.RegistrationDe1ete().Password,
            Ip: "216.28.34.2",
            UserAgent: "Google Chrome 111");

        var loginResult = await RequestAsync(loginCommand);

        loginResult.IsSuccess.Should().BeTrue();
    }
}