using AniMedia.Domain.Interfaces;

namespace AniMedia.Domain.Services;

public class DateTimeService : IDateTimeService {

    public DateTime Now {
        get {
            return DateTime.UtcNow;
        }
    }
}