using AniMedia.Domain.Abstracts;
using AniMedia.Domain.Entities.Validations;
using FluentValidation;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class UserEntity : IBaseEntity {
    public Guid UID { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Никнейм
    /// </summary>
    public string Nickname { get; set; } = default!;

    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? SecondName { get; set; }

    /// <summary>
    /// Ссылка на аватар
    /// </summary>
    public string? AvatarLink { get; set; }

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Соль к паролю
    /// </summary>
    public string PasswordSalt { get; set; } = default!;

    /// <summary>
    /// Сессии пользователя
    /// </summary>
    public virtual List<SessionEntity> Sessions { get; set; } = new();

    public UserEntity(string nickname, string passwordHash, string passwordSalt, string firstName = "", string secondName = "", string avatarLink = "") {
        Nickname = nickname;
        AvatarLink = avatarLink;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        FirstName = firstName;
        SecondName = secondName;

        new UserEntityValidator().ValidateAndThrow(this);
    }
}