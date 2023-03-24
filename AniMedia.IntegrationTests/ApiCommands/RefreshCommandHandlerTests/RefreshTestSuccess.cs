﻿using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.RefreshCommandHandlerTests;

public class RefreshTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var de1ete = await RequestAsync(CommandHelper.RegistrationDe1ete());
        var common = await RequestAsync(CommandHelper.RegistrationCommon());

        for (int i = 0; i < 9; i++) {
            var commonLoginCommand = new LoginCommand(
                CommandHelper.RegistrationCommon().Nickname,
                CommandHelper.RegistrationCommon().Password,
                CommandHelper.RandomIpAddress(),
                CommandHelper.RegistrationCommon().UserAgent);

            await RequestAsync(commonLoginCommand);
        }

        SetUser(de1ete.Value!.UID);

        var refreshDe1eteCommand = new RefreshCommand(
            de1ete.Value!.RefreshToken,
            CommandHelper.RandomIpAddress(),
            "Google Chrome 111");

        var refreshDe1eteResult = await RequestAsync(refreshDe1eteCommand);

        var getSessionDe1eteCommand = new GetSessionListQueryCommand();

        var resultSessionDe1ete = await RequestAsync(getSessionDe1eteCommand);

        ClearUser();

        SetUser(common.Value!.UID);

        var getSessionCommonCommand = new GetSessionListQueryCommand();

        var resultSessionCommon = await RequestAsync(getSessionCommonCommand);

        ClearUser();

        refreshDe1eteResult.Value!.AccessToken.Should().NotBe(de1ete.Value.AccessToken);
        refreshDe1eteResult.Value!.RefreshToken.Should().NotBe(de1ete.Value.RefreshToken);

        resultSessionDe1ete.Value!.Count.Should().Be(1);
        resultSessionCommon.Value!.Count.Should().Be(10);
    }
}