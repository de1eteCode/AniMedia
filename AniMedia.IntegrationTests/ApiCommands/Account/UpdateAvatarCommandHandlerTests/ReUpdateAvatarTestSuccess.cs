﻿using AniMedia.Application.ApiCommands.Account;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Account.UpdateAvatarCommandHandlerTests;

public class ReUpdateAvatarTestSuccess : IntegrationBinaryTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationDe1ete());

        SetUser(de1eteUser.Value!.UID);

        // request
        var rndBin = GetRandomData(Random.Shared.Next(2, 512));
        var fileName = "random.bin";
        var contentType = "bin/binary";
        using var ms = new MemoryStream();
        await ms.WriteAsync(rndBin, 0, rndBin.Length);
        ms.Position = 0;

        Result<BinaryFileDto> result = default!;

        for (int i = 0; i < 2; i++) {
            var request = new UpdateAvatarCommand(ms, fileName, contentType);

            result = await RequestAsync(request);
        }

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.UID.Should().NotBeEmpty();
        result.Value!.Name.Should().Be(fileName);
        result.Value!.ContentType.Should().Be(contentType);
    }
}