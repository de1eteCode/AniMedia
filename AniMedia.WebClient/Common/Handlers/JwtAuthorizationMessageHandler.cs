using AniMedia.WebClient.Common.Contracts;
using System.Net.Http.Headers;

namespace AniMedia.WebClient.Common.Handlers;

public class JwtAuthorizationMessageHandler : DelegatingHandler {
    private readonly ITokenService _tokenService;

    public JwtAuthorizationMessageHandler(ITokenService tokenService) {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        var currentToken = await _tokenService.GetTokenAsync(cancellationToken);

        if (string.IsNullOrEmpty(currentToken) == false) {
            var authHeader = new AuthenticationHeaderValue("Bearer", currentToken);

            request.Headers.Authorization = authHeader;
        }

        return await base.SendAsync(request, cancellationToken);
    }
}