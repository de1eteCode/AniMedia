﻿namespace AniMedia.Domain.Models.Responses;

public class Error {
    public string Message { get; set; }

    public Error(string message) {
        Message = message;
    }
}