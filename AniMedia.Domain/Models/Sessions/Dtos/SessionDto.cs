using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Sessions.Dtos;

public class SessionDto {

    public SessionDto() {
    }

    public SessionDto(SessionEntity session) {
        ArgumentNullException.ThrowIfNull(session);

        Uid = session.UID;
        Ip = session.Ip;
        UserAgent = session.UserAgent;
        CreateAt = session.CreateAt;
    }

    public DateTime CreateAt { get; set; }

    public string Ip { get; set; } = default!;

    public Guid Uid { get; set; }

    public string UserAgent { get; set; } = default!;
}