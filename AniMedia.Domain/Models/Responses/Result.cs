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

public class PagedResult<TItem>  {

    public PagedResult(int page, int pageSize, int pageCount, IEnumerable<TItem> value) {
        Page = page;
        PageSize = pageSize;
        PageCount = pageCount;
        Items = value;
    }

    public int Page { get; init; }

    public int PageCount { get; init; }

    public int PageSize { get; init; }

    public IEnumerable<TItem> Items { get; init; }
}