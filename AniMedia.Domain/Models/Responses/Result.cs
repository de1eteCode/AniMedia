namespace AniMedia.Domain.Models.Responses;

public class Result {

    public Result(Error error) {
        Error = error;
        IsSuccess = false;
    }

    public Result() {
    }

    public Error? Error { get; set; }

    public bool IsSuccess { get; set; } = true;
}

public class Result<TValue> : Result {

    public Result(Error error) : base(error) {
    }

    public Result(TValue value) {
        Value = value;
    }

    public TValue? Value { get; set; }
}