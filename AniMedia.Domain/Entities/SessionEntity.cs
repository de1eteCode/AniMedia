using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Сессия пользователя
/// </summary>
public class SessionEntity : BaseEntity {

    public SessionEntity(
        Guid userUid, 
        string accessToken, 
        string ip, 
        string userAgent, 
        DateTime expiresAt) {
        UserUid = userUid;
        AccessToken = accessToken;
        Ip = ip;
        UserAgent = userAgent;
        ExpiresAt = expiresAt;
    }

    public bool IsExpired {
        get {
            return DateTime.UtcNow >= ExpiresAt;
        }
    }

    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Дата создания сессии
    /// </summary>
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Время окончания сессии
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Ip адрес сессии
    /// </summary>
    public string Ip { get; set; } = default!;

    /// <summary>
    /// Рефрешь токен
    /// </summary>
    public Guid RefreshToken { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual UserEntity User { get; set; } = default!;

    /// <summary>
    /// Юзер агент пользователя
    /// </summary>
    public string UserAgent { get; set; } = default!;

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserUid { get; set; }

    public void UpdateAccessToken(string newAccessToken, double accessTokenLifeTimeInMinutes) {
        AccessToken = newAccessToken;
        ExpiresAt = DateTime.Now.AddMinutes(accessTokenLifeTimeInMinutes);
    }
}