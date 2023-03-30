using System.Security.Claims;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Constants;

namespace AniMedia.API.Common.Services;

public class CurrentUserService : ICurrentUserService {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserUID {
        get {
            var uidStr = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimConstants.UID);

            if (string.IsNullOrEmpty(uidStr)) return null;

            if (Guid.TryParse(uidStr, out var uid) == false) return null;

            return uid;
        }
    }
}