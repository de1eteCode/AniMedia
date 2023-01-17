using AniMedia.Application.Contracts.Identity;
using AniMedia.Application.Models.Identity;
using AniMedia.Identity.Configurations;
using AniMedia.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                UID = user.Id
            };
        }
        else {
            throw new Exception($"{result.Errors}");
        }
    }

    private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user) {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles) {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(CustomClaimTypes.UID, user.Id.ToString()),
        }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(_jwtSettings.GetKeyBytes());
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtToken;
    }
}