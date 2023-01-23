using AniMedia.Identity.Models;
using System.Security.Claims;

namespace AniMedia.Identity.Contracts;

internal interface ITokenService {

    /// <summary>
    /// Генерирование токена доступа
    /// </summary>
    /// <param name="user">Пользователь, для которого предназначен токе</param>
    /// <returns>Токен строкой</returns>
    public Task<string> GenerateAccessToken(ApplicationUser user);

    /// <summary>
    /// Генерирование токена доступа
    /// </summary>
    /// <param name="claims">Набор разрешений пользователя</param>
    /// <returns>Токен строкой</returns>
    public Task<string> GenerateAccessToken(IEnumerable<Claim> claims);

    /// <summary>
    /// Генерирование рефреш токена
    /// </summary>
    /// <returns>Рефреш токен строкой</returns>
    public Task<string> GenerateRefreshToken();
}