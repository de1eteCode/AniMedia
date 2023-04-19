using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Auth.RemoveSessionCommandHandlerTests;

public class RemoveSessionTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationCommandDe1ete());

        var loginIp = CommandHelper.RandomIpAddress();

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationCommandDe1ete().Nickname,
            CommandHelper.RegistrationCommandDe1ete().Password,
            loginIp,
            "Not Visual Studio 2022");

        await RequestAsync(loginCommand);

        SetUser(de1eteUser.Value!.UID);

        var getSessionQuery = new GetSessionListQueryCommand(1, 100);

        var sessions = await RequestAsync(getSessionQuery);

        var removeSessionCommand = new RemoveSessionCommand(de1eteUser.Value!.UID, sessions.Value!.OrderBy(e => e.CreateAt).First().Uid);

        await RequestAsync(removeSessionCommand);

        sessions = await RequestAsync(getSessionQuery);

        sessions.IsSuccess.Should().BeTrue();
        sessions.Value!.Count().Should().Be(1);
        sessions.Value!.First().Ip.Should().Be(loginIp);
    }
}