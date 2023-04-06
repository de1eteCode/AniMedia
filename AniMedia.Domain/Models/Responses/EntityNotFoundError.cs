using AniMedia.Domain.Constants;

namespace AniMedia.Domain.Models.Responses;

public class EntityNotFoundError : Error {

    public EntityNotFoundError(string message, int? code = default) : base(message, code) {
    }
    
    public EntityNotFoundError() : base("Entity not exists", ErrorCodesConstants.NotFound) {
    }
}