using AniMedia.Application.Models.Identity;
using AniMedia.Web.Models.ViewModels.Identity;
using AniMedia.Web.Services.Base;
using AniMedia.Web.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using IAuthService = AniMedia.Web.Contracts.IAuthenticationService;

namespace AniMedia.Web.Services;

internal class AuthenticationService : BaseService, IAuthService {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AuthenticationService(IApiClient api, IHttpContextAccessor httpContextAccessor, JwtSecurityTokenHandler jwtSecurityTokenHandler) : base(api) {
        _httpContextAccessor = httpContextAccessor;
        _tokenHandler = jwtSecurityTokenHandler;
    }

    public async Task<bool> Authenticate(LoginVM viewModel) {
        try {
            var request = new AuthorizationRequest() {
                UserName = viewModel.UserName,
                Password = viewModel.Password,
            };

            var responce = await _api.ApiV1AccountLoginAsync(request);

            if (responce == null || string.IsNullOrEmpty(responce.Token)) {
                return false;
            }

            var tokenContent = _tokenHandler.ReadJwtToken(responce.Token);
            var claims = ParseClaims(tokenContent);

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            var login = _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

            return true;
        }
        catch (Exception) {
            return false;
        }
    }

    public async Task<bool> Register(RegisterVM viewModel) {
        var request = new RegistrationRequest() {
            UserName = viewModel.UserName,
            FirstName = viewModel.FirstName,
            SecondName = viewModel.SecondName,
            Email = viewModel.Email,
            Password = viewModel.Password
        };

        var responce = await _api.ApiV1AccountRegisterAsync(request);

        if (responce == null || string.IsNullOrEmpty(responce.UserName)) {
            return false;
        }

        if (viewModel.LogInAfterRegister) {
            var authResult = await Authenticate(new LoginVM() {
                UserName = viewModel.UserName,
                Password = viewModel.Password,
            });

            return authResult;
        }

        return true;
    }

    public async Task Logout() {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private IEnumerable<Claim> ParseClaims(JwtSecurityToken tokenContent) {
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}