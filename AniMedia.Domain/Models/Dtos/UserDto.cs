using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Dtos;

public class UserDto {

    public UserDto() {
    }

    public UserDto(UserEntity user) {
        UID = user.UID;
        NickName = user.Nickname;
        AvatarLink = user.AvatarLink;
        FirstName = user.FirstName;
        SecondName = user.SecondName;
    }

    public string? AvatarLink { get; set; }

    public string? FirstName { get; set; }

    public string NickName { get; set; } = default!;

    public string? SecondName { get; set; }

    public Guid UID { get; set; }
}