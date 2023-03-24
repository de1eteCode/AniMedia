namespace AniMedia.Domain.Models.Responses;

public class Error {
    public string Message { get; set; } = default!;

    public Error() {
    }

    public Error(string message) {
        Message = message;
    }
}