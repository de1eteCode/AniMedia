using AniMedia.Domain.Abstracts;
using AniMedia.Domain.Entities.Validations;
using FluentValidation;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Сессия пользователя
/// </summary>
public class SessionEntity : IBaseEntity {

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid UID { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserUid { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual UserEntity User { get; set; } = default!;

    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Рефрешь токен
    /// </summary>
    public Guid RefreshToken { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Ip адрес сессии
    /// </summary>
    public string Ip { get; set; } = default!;

    /// <summary>
    /// Юзер агент пользователя
    /// </summary>
    public string UserAgent { get; set; } = default!;

    /// <summary>
    /// Время окончания сессии
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Дата создания сессии
    /// </summary>
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public SessionEntity(Guid userUid, string accessToken, string ip, string userAgent, DateTime expiresAt) {
        UserUid = userUid;
        AccessToken = accessToken;
        Ip = ip;
        UserAgent = userAgent;
        ExpiresAt = expiresAt;

        new SessionEntityValidator().ValidateAndThrow(this);
    }

    public void UpdateAccessToken(string newAccessToken, double accessTokenLifeTimeInMinutes) {
        AccessToken = newAccessToken;
        ExpiresAt = DateTime.Now.AddMinutes(accessTokenLifeTimeInMinutes);

        new SessionEntityValidator().ValidateAndThrow(this);
    }
}