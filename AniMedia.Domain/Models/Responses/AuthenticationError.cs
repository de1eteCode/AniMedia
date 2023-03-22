namespace AniMedia.Domain.Models.Responses;

public class AuthenticationError : Error {

    public AuthenticationError(string message) : base(message) {
    }
}