using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.BinaryFiles.Dtos;

namespace AniMedia.Domain.Models.Profiles.Dtos;

public class ProfileUserDto {

    public ProfileUserDto() {
    }

    public ProfileUserDto(UserEntity user) {
        ArgumentNullException.ThrowIfNull(user);

        UID = user.UID;
        NickName = user.Nickname;
        Avatar = user.Avatar != null ? new BinaryFileDto(user.Avatar) : default;
        FirstName = user.FirstName;
        SecondName = user.SecondName;
    }

    public BinaryFileDto? Avatar { get; set; }

    public string? FirstName { get; set; }

    public string NickName { get; set; } = default!;

    public string? SecondName { get; set; }

    public Guid UID { get; set; }
}