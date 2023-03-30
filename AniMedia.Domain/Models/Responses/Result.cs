namespace AniMedia.Domain.Models.Responses;

public class Result<TValue> {

    public Result(TValue value) {
        Value = value;
    }

    public Result(Error error) {
        Error = error;
        IsSuccess = false;
    }

    public Error? Error { get; set; }

    public bool IsSuccess { get; set; } = true;

    public TValue? Value { get; set; }
}