using AniMedia.Web.Models.ViewModels.Identity;
using System.Security.Claims;

namespace AniMedia.Web.Contracts;

internal interface IAuthenticationService {

    public Task<bool> Authenticate(LoginVM viewModel);

    public Task<bool> Register(RegisterVM viewModel);

    public Task<bool> IsSignedIn();

    public Task<IEnumerable<Claim>> GetClaims();

    public Task Logout();
}