using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Responses;

public class AuthorizationResponse {
    public string AccessToken { get; set; }

    public Guid RefreshToken { get; set; }

    public Guid UID { get; set; }

    public string NickName { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? AvatarLink { get; set; }

    public AuthorizationResponse(UserEntity user, string accessToken, Guid refreshToken) {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UID = user.UID;
        NickName = user.Nickname;
        AvatarLink = user.AvatarLink;
        FirstName = user.FirstName;
        SecondName = user.SecondName;
    }
}