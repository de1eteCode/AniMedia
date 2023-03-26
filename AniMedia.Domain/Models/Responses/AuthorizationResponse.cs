namespace AniMedia.Domain.Models.Responses;

public class AuthorizationResponse {
    public string AccessToken { get; set; } = default!;

    public Guid RefreshToken { get; set; } = default!;

    public Guid UID { get; set; } = default!;

    public AuthorizationResponse() {
    }

    public AuthorizationResponse(Guid userUid, string accessToken, Guid refreshToken) {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        UID = userUid;
    }
}