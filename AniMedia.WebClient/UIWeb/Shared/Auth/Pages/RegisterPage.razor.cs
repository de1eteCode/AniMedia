using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Providers;
using AniMedia.WebClient.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.UIWeb.Shared.Auth.Pages;

[AllowAnonymous]
public partial class RegisterPage : ComponentBase {
    private readonly RegistrationVM _vmodel = new();

    [Inject]
    public JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public string ReturnUrl { get; set; } = string.Empty;

    private async Task RegisterUser() {
        var result = await AuthStateProvider.Registration(_vmodel);

        if (result) {
            if (string.IsNullOrEmpty(ReturnUrl) == false) // redirect to url
            {
                NavigationManager.NavigateTo(ReturnUrl);
            }
            else // redirect to home
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}