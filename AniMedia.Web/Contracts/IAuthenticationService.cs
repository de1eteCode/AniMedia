using AniMedia.Web.Models.ViewModels.Identity;

namespace AniMedia.Web.Contracts;

internal interface IAuthenticationService {

    public Task<bool> Authenticate(LoginVM viewModel);

    public Task<bool> Register(RegisterVM viewModel);

    public Task Logout();
}