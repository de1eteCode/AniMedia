using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.Auth.GetSessionListQueryHandlerTests;

public class GetSessionListTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationCommandDe1ete());

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationCommandDe1ete().Nickname,
            CommandHelper.RegistrationCommandDe1ete().Password,
            CommandHelper.RegistrationCommandDe1ete().Ip,
            CommandHelper.RegistrationCommandDe1ete().UserAgent);

        await RequestAsync(loginCommand);

        SetUser(de1eteUser.Value!.UID);

        var getSessionsQuery = new GetSessionListQueryCommand();

        var result = await RequestAsync(getSessionsQuery);

        result.Value!.Count.Should().Be(2);
    }
}