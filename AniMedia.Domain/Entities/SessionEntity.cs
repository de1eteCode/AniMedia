using AniMedia.Domain.Abstracts;

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
    public UserEntity User { get; set; } = default!;

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
}