using AniMedia.Application.ApiCommands.Account;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Account.UpdateProfileCommandHandlerTests;

public class UpdateProfileTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationDe1ete());

        SetUser(de1eteUser.Value!.UID);

        //request
        var fName = CommandHelper.GetRandomString();
        var sName = CommandHelper.GetRandomString();

        var request = new UpdateProfileCommand(fName, sName);

        var result = await RequestAsync(request);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.FirstName.Should().Be(fName);
        result.Value!.SecondName.Should().Be(sName);
    }
}