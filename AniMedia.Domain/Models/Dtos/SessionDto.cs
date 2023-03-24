using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class SessionDto {
    public Guid Uid { get; set; } = default!;
    public string Ip { get; set; } = default!;
    public string UserAgent { get; set; } = default!;
    public DateTime CreateAt { get; set; } = default!;

    public SessionDto() {
    }

    public SessionDto(SessionEntity session) {
        Uid = session.UID;
        Ip = session.Ip;
        UserAgent = session.UserAgent;
        CreateAt = session.CreateAt;
    }
}