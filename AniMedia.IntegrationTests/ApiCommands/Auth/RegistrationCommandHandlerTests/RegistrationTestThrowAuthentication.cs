using AniMedia.Domain.Models.Responses;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Auth.RegistrationCommandHandlerTests;

public class RegistrationTestThrowAuthentication : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        var reg1Command = CommandHelper.RegistrationDe1ete();

        await RequestAsync(reg1Command);

        var reg2Command = CommandHelper.RegistrationDe1ete();

        var result = await RequestAsync(reg2Command);

        result.Error.Should().BeOfType<RegistrationError>();
    }
}