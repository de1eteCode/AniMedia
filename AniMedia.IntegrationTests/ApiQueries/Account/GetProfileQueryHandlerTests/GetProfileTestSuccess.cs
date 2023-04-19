using AniMedia.Application.ApiQueries.Account;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiQueries.Account.GetProfileQueryHandlerTests;

public class GetProfileTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationCommandDe1ete());

        // request
        var requestProfile = new GetProfileQueryCommand(de1eteUser.Value!.UID);

        var result = await RequestAsync(requestProfile);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.NickName.Should().Be(CommandHelper.RegistrationCommandDe1ete().Nickname);
        result.Value!.UID.Should().Be(de1eteUser.Value!.UID);
    }
}