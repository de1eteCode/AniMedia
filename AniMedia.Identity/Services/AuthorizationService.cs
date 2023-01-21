using AniMedia.Application.Exceptions;
using AniMedia.Application.Models.Identity;
using AniMedia.Identity.Configurations;
using AniMedia.Identity.Contracts;
using AniMedia.Identity.Exceptions;
using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace AniMedia.Identity.Services;

internal class AuthorizationService : IAuthorizationService {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthorizationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService) {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<AuthorizationResponce> Login(AuthorizationRequest request) {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null) {
            throw new BadRequestException($"User with '{request.UserName}' username not found");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (signInResult.Succeeded == false) {
            throw new BadRequestException($"Invalid password for '{request.UserName}'");
        }

        var tokenPair = await _tokenService.GenerateTokenPair(user);

        return new AuthorizationResponce {
            UserName = user.UserName!,
            Tokens = tokenPair
        };
    }

    public async Task<RegistrationResponce> Register(RegistrationRequest request) {
        var userIsExists = await _userManager.FindByNameAsync(request.UserName);

        if (userIsExists != null) {
            throw new BadRequestException($"User with '{request.UserName}' already exists");
        }

        userIsExists = await _userManager.FindByEmailAsync(request.Email);

        if (userIsExists != null) {
            throw new BadRequestException($"User with email '{request.Email}' already exists");
        }

        var user = new ApplicationUser {
            UserName = request.UserName,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded) {
            await _userManager.AddToRoleAsync(user, RoleConfiguration.USER.Name);

            return new RegistrationResponce {
                UserName = user.UserName,
            };
        }
        else {
            throw new IdentityException(result);
        }
    }
}