﻿using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.RegistrationCommandHandlerTests;

public class RegistrationTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var regCommand = CommandHelper.RegistrationDe1ete();

        var result = await RequestAsync(regCommand);

        result.IsSuccess.Should().BeTrue();
    }
}