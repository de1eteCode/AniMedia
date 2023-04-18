namespace AniMedia.Domain.Models.Responses;

public class Result {

    public Result(Error error) {
        Error = error;
        IsSuccess = false;
    }

    public Result() {
    }

    public Error? Error { get; }

    public bool IsSuccess { get; } = true;
}

public class Result<TValue> : Result {

    public Result(Error error) : base(error) {
    }

    public Result(TValue value) {
        Value = value;
    }

    public TValue? Value { get; }
}

public class PagedResult<TValue> : Result<IEnumerable<TValue>> {

    public PagedResult(Error error) : base(error) {
        PageSize = -1;
        Page = -1;
        PageCount = -1;
    }

    public PagedResult(int page, int pageSize, int pageCount, IEnumerable<TValue> value) : base(value) {
        Page = page;
        PageSize = pageSize;
        PageCount = pageCount;
    }

    public int Page { get; init; }

    public int PageCount { get; init; }

    public int PageSize { get; init; }

    public int Items {
        get {
            if (Error != null) {
                return -1;
            }

            return Value?.Count() ?? 0;
        }
    }
}