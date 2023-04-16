using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Abstracts;

public class BaseAuditableEntity : BaseEntity {

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreateAt { get; set; }
    
    /// <summary>
    /// Дата последнего редактирования
    /// </summary>
    public DateTime? LastModified { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, кем был создан объект
    /// </summary>
    public Guid? CreateByUid { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, кем был отредактирован объект в последний раз
    /// </summary>
    public Guid? LastModifiedByUid { get; set; }
    
    /// <summary>
    /// Пользователь, который создал объект
    /// </summary>
    public virtual UserEntity? CreateBy { get; set; }
    
    /// <summary>
    /// Пользователь, который отредактировал объект в последний раз
    /// </summary>
    public virtual UserEntity? LastModifiedBy { get; set; }
}