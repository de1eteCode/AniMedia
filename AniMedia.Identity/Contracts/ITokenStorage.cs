namespace AniMedia.Identity.Contracts;

internal interface ITokenStorage {

    /// <summary>
    /// Сохранение рефреш токена
    /// </summary>
    /// <param name="refreshToken">Рефреш токен</param>
    /// <param name="expiredAt">Дата просрочки</param>
    public Task SaveRefreshToken(string refreshToken, DateTime expiredAt);

    /// <summary>
    /// Получение рефреш токена из сохраненных ранее
    /// </summary>
    /// <param name="refreshToken">Рефреш токен</param>
    /// <param name="pair">Пара рефреш токена и даты просрочки</param>
    /// <returns>Результат нахождения</returns>
    public Task<bool> TryGetRefreshToken(string refreshToken, out KeyValuePair<string, DateTime> pair);

    /// <summary>
    /// Удаление рефреш токена
    /// </summary>
    /// <param name="refreshToken">Рефреш токен на удаление</param>
    /// <returns>True - успешно удален</returns>
    public Task<bool> TryRemoveRefreshToken(string refreshToken);
}