using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

public class WeatherForecast : IBaseEntity {

    public int TemperatureF {
        get {
            return 32 + (int)(TemperatureC / 0.5556);
        }
    }

    public DateTime Date { get; set; }

    public string? Summary { get; set; }

    public int TemperatureC { get; set; }

    public Guid UID { get; set; } = Guid.NewGuid();
}