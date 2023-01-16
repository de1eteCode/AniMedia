using AniMedia.Application.Contracts.Identity;
using AniMedia.Application.Models.Identity;
using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniMedia.Identity.Services;

internal class AuthorizationService : IAuthorizationService {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthorizationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings) {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthorizationResponce> Login(AuthorizationRequest request) {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null) {
            throw new Exception($"User with '{request.UserName}' username not found");
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (signInResult.Succeeded == false) {
            throw new Exception($"Invalid password for '{request.UserName}'");
        }

        var jwtToken = await GenerateJwtToken(user);

        return new AuthorizationResponce {
            UID = user.Id,
            UserName = user.UserName!,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
        };
    }

    public async Task<RegistrationResponce> Register(RegistrationRequest request) {
        var userIsExists = await _userManager.FindByNameAsync(request.UserName);

        if (userIsExists != null) {
            throw new Exception($"User with '{request.UserName}' already exists");
        }

        userIsExists = await _userManager.FindByEmailAsync(request.Email);

        if (userIsExists != null) {
            throw new Exception($"User with email '{request.Email}' already exists");
        }

        var newUser = new ApplicationUser {
            UserName = request.UserName,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(newUser);

        if (result.Succeeded) {
            /// Todo:
            ///     Add role

            throw new NotImplementedException();

#pragma warning disable CS0162 // Обнаружен недостижимый код
            return new RegistrationResponce {
                UID = userIsExists.Id
            };
#pragma warning restore CS0162 // Обнаружен недостижимый код
        }
        else {
            throw new Exception($"{result.Errors}");
        }
    }

    private Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user) {
        throw new NotImplementedException();
    }
}