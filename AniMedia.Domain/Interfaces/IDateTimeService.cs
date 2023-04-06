namespace AniMedia.Domain.Interfaces;

public interface IDateTimeService {

    /// <summary>
    /// Текущее время
    /// </summary>
    public DateTime Now { get; }
}