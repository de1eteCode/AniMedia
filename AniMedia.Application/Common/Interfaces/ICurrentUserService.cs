namespace AniMedia.Application.Common.Interfaces;

public interface ICurrentUserService {
    public Guid? UserUID { get; }

    public bool IsAuthenticated {
        get {
            return UserUID != null && UserUID != Guid.Empty;
        }
    }
}