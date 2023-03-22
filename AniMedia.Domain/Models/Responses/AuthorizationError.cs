namespace AniMedia.Domain.Models.Responses;

public class AuthorizationError : Error {

    public AuthorizationError(string message) : base(message) {
    }
}