namespace AniMedia.WebClient.Common.Contracts;

public interface ITokenService {

    public ValueTask<string> GetTokenAsync(CancellationToken cancellationToken = default);

    public ValueTask SetTokenAsync(string token, CancellationToken cancellationToken = default);

    public ValueTask DeleteTokenAsync(CancellationToken cancellationToken = default);

    public ValueTask<Guid> GetRefreshTokenAsync(CancellationToken cancellationToken = default);

    public ValueTask SetRefreshTokenAsync(Guid refreshtoken, CancellationToken cancellationToken = default);

    public ValueTask DeleteRefreshTokenAsync(CancellationToken cancellationToken = default);
}