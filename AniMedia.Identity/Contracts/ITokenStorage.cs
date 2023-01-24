namespace AniMedia.Identity.Contracts;

internal interface ITokenStorage {

    /// <summary>
    /// Сохранение рефреш токена
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="refreshToken">Рефреш токен</param>
    /// <param name="expiredAt">Дата просрочки</param>
    public Task SaveRefreshToken(string username, string refreshToken, DateTime expiredAt);

    /// <summary>
    /// Получение рефреш токена из сохраненных ранее
    /// </summary>
    /// <param name="refreshToken">Рефреш токен</param>
    /// <param name="pair"></param>
    /// <returns>Результат нахождения и пара рефреш токена и даты просрочки</returns>
    public Task<(bool IsFinded, KeyValuePair<string, DateTime> Pair)> TryGetRefreshToken(string username, string refreshToken);

    /// <summary>
    /// Удаление рефреш токена
    /// </summary>
    /// <param name="refreshToken">Рефреш токен на удаление</param>
    /// <returns>True - успешно удален</returns>
    public Task<bool> TryRemoveRefreshToken(string username, string refreshToken);
}