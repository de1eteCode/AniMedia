﻿namespace AniMedia.Domain.Models.Requests;

public class RegistrationRequest {

    public string Nickname { get; set; } = default!;

    public string Password { get; set; } = default!;
}