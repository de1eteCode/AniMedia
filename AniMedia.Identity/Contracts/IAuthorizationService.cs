using AniMedia.Application.Models.Identity;

namespace AniMedia.Identity.Contracts;

public interface IAuthorizationService {

    Task<AuthorizationResponce> Login(AuthorizationRequest request);

    Task<RegistrationResponce> Register(RegistrationRequest request);
}