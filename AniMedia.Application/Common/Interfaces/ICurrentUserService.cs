namespace AniMedia.Application.Common.Interfaces;

public interface ICurrentUserService {
    public Guid? UserUID { get; }
}