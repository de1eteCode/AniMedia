using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.GetSessionListQueryHandlerTests;

public class GetSessionListTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationDe1ete());

        var loginCommand = new LoginCommand(
            CommandHelper.RegistrationDe1ete().Nickname,
            CommandHelper.RegistrationDe1ete().Password,
            CommandHelper.RegistrationDe1ete().Ip,
            CommandHelper.RegistrationDe1ete().UserAgent);

        await RequestAsync(loginCommand);

        SetUser(de1eteUser.Value!.UID);

        var getSessionsQuery = new GetSessionListQueryCommand();

        var result = await RequestAsync(getSessionsQuery);

        result.Value!.Count.Should().Be(2);
    }
}