namespace AniMedia.Domain.Models.Responses;

public class AuthorizationResponse {

    public AuthorizationResponse() {
    }

    public AuthorizationResponse(Guid userUid, string accessToken, Guid refreshToken) {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UID = userUid;
    }

    public string AccessToken { get; set; } = default!;

    public Guid RefreshToken { get; set; }

    public Guid UID { get; set; }
}