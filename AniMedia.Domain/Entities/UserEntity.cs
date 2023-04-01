using AniMedia.Domain.Abstracts;
using AniMedia.Domain.Entities.Validations;
using FluentValidation;

namespace AniMedia.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class UserEntity : IBaseEntity {

    public UserEntity(string nickname, string passwordHash, string passwordSalt, string firstName = "",
        string secondName = "") {
        Nickname = nickname;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        FirstName = firstName;
        SecondName = secondName;

        new UserEntityValidator().ValidateAndThrow(this);
    }

    /// <summary>
    /// Аватар
    /// </summary>
    public virtual BinaryFileEntity? Avatar { get; set; }

    /// <summary>
    /// Идентификатор аватарки
    /// </summary>
    public Guid? AvatarFileUID { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Никнейм
    /// </summary>
    public string Nickname { get; set; } = default!;

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Соль к паролю
    /// </summary>
    public string PasswordSalt { get; set; } = default!;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? SecondName { get; set; }

    /// <summary>
    /// Сессии пользователя
    /// </summary>
    public virtual List<SessionEntity> Sessions { get; set; } = new();

    public Guid UID { get; set; } = Guid.NewGuid();
}