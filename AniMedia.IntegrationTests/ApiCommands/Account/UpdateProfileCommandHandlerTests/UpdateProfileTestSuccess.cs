using AniMedia.Application.ApiCommands.Account;
using AniMedia.Domain.Models.Profiles.Requests;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.Account.UpdateProfileCommandHandlerTests;

public class UpdateProfileTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        var de1eteUser = await RequestAsync(CommandHelper.RegistrationCommandDe1ete());

        SetUser(de1eteUser.Value!.UID);

        //request
        var request = new UpdateProfileCommand(new UpdateProfileRequest() {
            FirstName = CommandHelper.GetRandomString(),
            SecondName = CommandHelper.GetRandomString()
        });

        var result = await RequestAsync(request);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.FirstName.Should().Be(request.Model.FirstName);
        result.Value!.SecondName.Should().Be(request.Model.SecondName);
    }
}