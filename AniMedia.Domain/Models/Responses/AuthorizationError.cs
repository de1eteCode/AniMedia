namespace AniMedia.Domain.Models.Responses;

public class AuthorizationError : Error {

    public AuthorizationError() {
    }

    public AuthorizationError(string message, int? code = default) : base(message, code) {
    }
}