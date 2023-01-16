using AniMedia.Application.Models.Identity;

namespace AniMedia.Application.Contracts.Identity;

public interface IAuthorizationService {

    Task<AuthorizationResponce> Login(AuthorizationRequest request);

    Task<RegistrationResponce> Register(RegistrationRequest request);
}