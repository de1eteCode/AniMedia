using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.Auth.GetSessionQueryHandlerTests;

public class GetSessionTestSuccess : IntegrationTestBase
{

    [Fact]
    public override async Task Test()
    {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationDe1ete());

        var loginIp = CommandHelper.RandomIpAddress();

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationDe1ete().Nickname,
            CommandHelper.RegistrationDe1ete().Password,
            loginIp,
            CommandHelper.RegistrationDe1ete().UserAgent);

        var loginResult = await RequestAsync(loginCommand);

        SetUser(de1eteUser.Value!.UID);

        var getSessionQuery = new GetSessionQueryCommand(loginResult.Value!.AccessToken);

        var result = await RequestAsync(getSessionQuery);

        result.Value!.Ip.Should().Be(loginIp);
    }
}