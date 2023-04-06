namespace AniMedia.Domain.Models.Responses;

public class RegistrationError : Error {

    public RegistrationError(string message, int? code = default) : base(message, code) {
    }
}