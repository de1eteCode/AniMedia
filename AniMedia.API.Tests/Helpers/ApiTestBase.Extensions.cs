using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiQueries.Account;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.API.Tests.Helpers;

public static class ApiTestBaseExtensions {

    public record UserTest(ProfileUserDto Profile, string Password);
    
    public record LoggedUserTest(ProfileUserDto Profile, string Password, string AccessToken, Guid RefreshToken) : UserTest(Profile, Password);

    public static async Task<LoggedUserTest> GetLoggedRegisteredUser(this ApiTestBase apiTestBase) {
        await using var scope = apiTestBase.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        // создание пользователя
        var regCommand = new RegistrationCommand(
            CommandHelper.GetRandomString(),
            CommandHelper.GetRandomString(),
            CommandHelper.RandomIpAddress(),
            CommandHelper.GetRandomString()
        );
        var resAuth = await mediator.Send(regCommand);

        // получение профиля
        var profileReq = new GetProfileQueryCommand(resAuth.Value!.UID);
        var resProfile = await mediator.Send(profileReq);
        
        return new LoggedUserTest(resProfile.Value!, regCommand.Password, resAuth.Value.AccessToken, resAuth.Value.RefreshToken);
    }

    public static async Task<UserTest> GetRegisteredUser(this ApiTestBase apiTestBase) {
        await using var scope = apiTestBase.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logged = await apiTestBase.GetLoggedRegisteredUser();
        
        // получение сессии
        var getSessionReq = new GetSessionQueryCommand(logged.Profile.UID, logged.AccessToken);
        var resSessionReq = await mediator.Send(getSessionReq);
        
        // удаление сессии
        var removeSessionReq = new RemoveSessionCommand(logged.Profile.UID, resSessionReq.Value!.Uid);
        var resRemoveSessionReq = await mediator.Send(removeSessionReq);
        
        return new UserTest(logged.Profile, logged.Password);
    }
}