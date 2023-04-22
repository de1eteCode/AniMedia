using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.Auth.GetSessionQueryHandlerTests;

public class GetSessionTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationCommandDe1ete());

        var loginIp = CommandHelper.RandomIpAddress();

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationCommandDe1ete().Nickname,
            CommandHelper.RegistrationCommandDe1ete().Password,
            loginIp,
            CommandHelper.RegistrationCommandDe1ete().UserAgent);

        var loginResult = await RequestAsync(loginCommand);

        var getSessionQuery = new GetSessionQueryCommand(de1eteUser.Value.UID, loginResult.Value!.AccessToken);

        var result = await RequestAsync(getSessionQuery);

        result.Value!.Ip.Should().Be(loginIp);
    }
}