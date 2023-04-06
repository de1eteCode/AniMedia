using AniMedia.Domain.Constants;

namespace AniMedia.Domain.Models.Responses;

public class Error {

    public Error() {
    }

    public Error(string message, int? code = default) {
        Message = message;

        if (code != null) {
            Code = code.GetValueOrDefault();
        }
    }

    public int Code { get; set; } = ErrorCodesConstants.NoSetCode;
    
    public string Message { get; set; } = default!;
}