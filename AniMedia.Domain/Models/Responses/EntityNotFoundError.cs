namespace AniMedia.Domain.Models.Responses;

public class EntityNotFoundError : Error {

    public EntityNotFoundError(string message) : base(message) {
    }

    public EntityNotFoundError() : base("Entity not exists") {
    }
}