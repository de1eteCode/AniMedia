namespace AniMedia.Domain.Models.Responses;

public class AuthenticationError : Error {

    public AuthenticationError(string message, int? code = default) : base(message, code) {
    }
}