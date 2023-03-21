using AniMedia.Application.Common.Models.Identity;
using AniMedia.Identity.Models;
using System.Security.Claims;

namespace AniMedia.Identity.Contracts;

public interface ITokenService {

    /// <summary>
    /// Генерирование пары токенов
    /// </summary>
    /// <param name="user">Пользователь, для которого предназначены токены</param>
    /// <returns>Пара токенов доступа и рефреша</returns>
    internal Task<TokenPair> GenerateTokenPair(ApplicationUser user);

    /// <summary>
    /// Генерирование пары токенов
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="claims">Набор разрешений пользователя</param>
    /// <returns>Пара токенов доступа и рефреша</returns>
    public Task<TokenPair> GenerateTokenPair(string username, IEnumerable<Claim> claims);

    /// <summary>
    /// Генерирование пары токенов
    /// </summary>
    /// <param name="expiredPair">Пара с просроченным токеном</param>
    /// <returns>Пара токенов доступа и рефреша</returns>
    public Task<TokenPair> GenerateTokenPair(TokenPair expiredPair);
}