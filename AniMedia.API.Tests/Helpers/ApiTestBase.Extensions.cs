using AniMedia.Application.ApiCommands.AnimeSeries;
using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Application.ApiCommands.Binary;
using AniMedia.Application.ApiCommands.Genres;
using AniMedia.Application.ApiQueries.Account;
using AniMedia.Application.ApiQueries.Auth;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AniMedia.API.Tests.Helpers;

public static class ApiTestBaseExtensions {

    public record UserTest(ProfileUserDto Profile, string Password);
    
    public record LoggedUserTest(ProfileUserDto Profile, string Password, string AccessToken, Guid RefreshToken) : UserTest(Profile, Password);

    /// <summary>
    /// Создание авторизированного пользователя
    /// </summary>
    /// <param name="apiTestBase"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Создание пользователя с удаленными сессиями
    /// </summary>
    /// <param name="apiTestBase"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Создание сессии для пользователя
    /// </summary>
    /// <param name="apiTestBase"></param>
    /// <param name="user">Пользователь</param>
    /// <returns></returns>
    public static async Task<SessionDto> CreateSession(this ApiTestBase apiTestBase, UserTest user) {
        await using var scope = apiTestBase.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var loginReq = new LoginCommand(
            user.Profile.NickName,
            user.Password,
            CommandHelper.RandomIpAddress(),
            CommandHelper.GetRandomString());

        var authResp = await mediator.Send(loginReq);

        var getSessionReq = new GetSessionQueryCommand(user.Profile.UID, authResp.Value!.AccessToken);
        var getSessionResp = await mediator.Send(getSessionReq);

        return getSessionResp.Value!;
    }

    /// <summary>
    /// Создание файла
    /// </summary>
    /// <param name="apiTestBase"></param>
    /// <param name="contentType">Тип контента</param>
    /// <param name="fileExtension">Расширение файла</param>
    /// <returns></returns>
    public static async Task<BinaryFileDto> CreateFile(this ApiTestBase apiTestBase, string contentType, string fileExtension) {
        await using var scope = apiTestBase.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await using var mockFileBuilder = new MockFileBuilder();

        var mockFile = await mockFileBuilder
            .SetContentSize(32)
            .SetContentType(contentType)
            .SetFileExtension(fileExtension)
            .Build();
        
        var req = new SaveBinaryFileCommand(mockFile.Data, contentType);
        var res = await mediator.Send(req);

        return res.Value!;
    }

    /// <summary>
    /// Создание жанра
    /// </summary>
    /// <param name="apiTestBase"></param>
    /// <returns></returns>
    public static async Task<GenreDto> CreateGenre(this ApiTestBase apiTestBase) {
        await using var scope = apiTestBase.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var createCommand = new AddGenreCommand(CommandHelper.GetRandomString());
        var res = await mediator.Send(createCommand);

        return res.Value!;
    }

    /// <summary>
    /// Создание аниме серии
    /// </summary>
    /// <param name="apiTestBase"></param>
    /// <returns></returns>
    public static async Task<AnimeSeriesDto> CreateAnimeSeries(this ApiTestBase apiTestBase) {
        await using var scope = apiTestBase.ServiceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var genres = new List<GenreDto>();

        for (var i = 0; i < 2; i++) {
            genres.Add(await apiTestBase.CreateGenre());
        }

        var announcement = Random.Shared.Next(600, 6000);
        var createCommand = new AddAnimeSeriesCommand {
            Name = CommandHelper.GetRandomString(),
            EnglishName = CommandHelper.GetRandomString(),
            JapaneseName = CommandHelper.GetRandomString(),
            Description = CommandHelper.GetRandomString(20, 100),
            Genres = genres,
            DateOfAnnouncement = DateTime.Now.AddDays(-1 * announcement),
            DateOfRelease = DateTime.Now.AddDays(-1 * Random.Shared.Next(1, announcement)),
            ExistTotalEpisodes = Random.Shared.Next(100, 300),
            PlanedTotalEpisodes = Random.Shared.Next(300, 1500)
        };

        var res = await mediator.Send(createCommand);

        return res.Value!;
    }
}