using AniMedia.Web.Contracts;
using AniMedia.Web.Models.ViewModels.Identity;
using AniMedia.Web.Services.Base;
using AniMedia.Web.Services.Contracts;

namespace AniMedia.Web.Services;

internal class AuthenticationService : BaseService, IAuthenticationService {

    public AuthenticationService(IApiClient api) : base(api) {
    }

    public Task<bool> Authenticate(LoginVM viewModel) {
        throw new NotImplementedException();
    }

    public Task Logout() {
        throw new NotImplementedException();
    }

    public Task<bool> Register(RegisterVM viewModel) {
        throw new NotImplementedException();
    }
}