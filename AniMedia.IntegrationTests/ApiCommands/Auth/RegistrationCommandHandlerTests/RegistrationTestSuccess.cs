using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Auth.RegistrationCommandHandlerTests;

public class RegistrationTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var regCommand = CommandHelper.RegistrationCommandDe1ete();

        var result = await RequestAsync(regCommand);

        result.IsSuccess.Should().BeTrue();
    }
}