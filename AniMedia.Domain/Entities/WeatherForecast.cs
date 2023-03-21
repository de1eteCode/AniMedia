using AniMedia.Domain.Abstracts;

namespace AniMedia.Domain.Entities;

public class WeatherForecast : IBaseEntity {
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public Guid UID { get; set; } = Guid.NewGuid();
}