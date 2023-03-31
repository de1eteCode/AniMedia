using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Providers;
using AniMedia.WebClient.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Pages.Account;

[AllowAnonymous]
public partial class Register : ComponentBase {
    private readonly RegistrationVM _vmodel = new();

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    public JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public string ReturnUrl { get; set; } = string.Empty;

    private async Task RegisterUser() {
        var result = await AuthenticationService.Registration(_vmodel);

        if (result) {
            AuthStateProvider.NotifyAuthenticationStateChanged();

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