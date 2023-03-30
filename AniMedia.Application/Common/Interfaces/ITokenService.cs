using System.IdentityModel.Tokens.Jwt;
using AniMedia.Domain.Entities;

namespace AniMedia.Application.Common.Interfaces;

public interface ITokenService {

    /// <summary>
    /// Создание токена доступа
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Токен доступа для <paramref name="user" /></returns>
    public string CreateAccessToken(UserEntity user);

    /// <summary>
    /// Проверка корректности токена
    /// </summary>
    /// <param name="token">Токен для проверки</param>
    /// <param name="signKey">Секретный ключ</param>
    /// <param name="validatedJwtToken">Проверенный токен</param>
    /// <returns>True - токен валидный</returns>
    public bool TryValidateAccessToken(string token, out JwtSecurityToken validatedJwtToken);
}