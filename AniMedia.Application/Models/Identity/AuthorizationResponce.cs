﻿namespace AniMedia.Application.Models.Identity;

public class AuthorizationResponce {
    public required Guid UID { get; init; }

    public required string UserName { get; init; }

    public required string Token { get; init; }
}