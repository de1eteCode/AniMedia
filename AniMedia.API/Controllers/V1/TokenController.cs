using AniMedia.Application.Models.Identity;
using AniMedia.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[AllowAnonymous]
public class TokenController : BaseApiV1Controller {
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService) {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<UpdateTokenResponce> UpdateToken(UpdateTokenRequest request) =>
        new UpdateTokenResponce() {
            Tokens = await _tokenService.GenerateTokenPair(request.Tokens)
        };
}