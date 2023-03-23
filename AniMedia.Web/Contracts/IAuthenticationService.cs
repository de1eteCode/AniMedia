using AniMedia.Web.Models.ViewModels.Identity;
using System.Security.Claims;

namespace AniMedia.Web.Contracts;

public interface IAuthenticationService {

    public Task<bool> Authenticate();

    /// <summary>
    /// Авторизация пользователя в системе
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public Task<bool> Login(LoginVM viewModel);

    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    public Task<bool> Register(RegisterVM viewModel);

    /// <summary>
    /// Выход пользователя из системы
    /// </summary>
    /// <returns></returns>
    public Task Logout();

    /// <summary>
    /// Список разрешений пользователя
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Claim>> GetClaims();

    /// <summary>
    /// Выполнен вход в систему
    /// </summary>
    /// <returns></returns>
    public Task<bool> IsSignedIn();
}