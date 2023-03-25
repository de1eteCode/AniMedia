using AniMedia.WebClient.Common.ViewModels;

namespace AniMedia.WebClient.Common.Contracts;

public interface IAuthenticationService {

    public Task<bool> Login(LoginVM viewModel);

    public Task<bool> Registration(RegistrationVM viewModel);

    public Task Logout();
}