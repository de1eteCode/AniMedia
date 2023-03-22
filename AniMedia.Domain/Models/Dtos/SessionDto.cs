using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class SessionDto {
    public Guid Uid { get; set; }
    public string Ip { get; set; }
    public string UserAgent { get; set; }
    public DateTime CreateAt { get; set; }

    public SessionDto(SessionEntity session) {
        Uid = session.UID;
        Ip = session.Ip;
        UserAgent = session.UserAgent;
        CreateAt = session.CreateAt;
    }
}