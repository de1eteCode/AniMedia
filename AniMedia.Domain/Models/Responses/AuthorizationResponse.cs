﻿using AniMedia.Domain.Entities;

namespace AniMedia.Domain.Models.Responses;

public class AuthorizationResponse {
    public string AccessToken { get; set; } = default!;

    public Guid RefreshToken { get; set; } = default!;

    public Guid UID { get; set; } = default!;

    public string NickName { get; set; } = default!;

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? AvatarLink { get; set; }

    public AuthorizationResponse() {
    }

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