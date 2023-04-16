namespace AniMedia.Domain.Abstracts;

public class BaseEntity {

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid UID { get; set; } = Guid.NewGuid();
}