namespace AniMedia.Domain.Models.Responses;

public class RegistrationError : Error {

    public RegistrationError(string message) : base(message) {
    }
}