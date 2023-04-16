﻿using AniMedia.Application.ApiCommands.Auth;
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
            CommandHelper.LoginRequestDe1ete(),
            loginIp,
            CommandHelper.RegistrationCommandDe1ete().UserAgent);

        await RequestAsync(loginCommand);

        SetUser(de1eteUser.Value!.UID);

        var getSessionQuery = new GetSessionListQueryCommand();

        var sessions = await RequestAsync(getSessionQuery);

        var removeSessionCommand = new RemoveSessionCommand(sessions.Value!.DistinctBy(e => e.CreateAt).First().Uid);

        await RequestAsync(removeSessionCommand);

        sessions = await RequestAsync(getSessionQuery);

        sessions.IsSuccess.Should().BeTrue();
        sessions.Value!.Count.Should().Be(1);
        sessions.Value!.First().Ip.Should().Be(loginIp);
    }
}