using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class UserDto {
    public Guid UID { get; set; } = default!;

    public string NickName { get; set; } = default!;

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? AvatarLink { get; set; }

    public UserDto() {
    }

    public UserDto(UserEntity user) {
        UID = user.UID;
        NickName = user.Nickname;
        AvatarLink = user.AvatarLink;
        FirstName = user.FirstName;
        SecondName = user.SecondName;
    }
}