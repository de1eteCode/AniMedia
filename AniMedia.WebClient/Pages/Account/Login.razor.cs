using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Providers;
using AniMedia.WebClient.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Pages.Account;

[AllowAnonymous]
public partial class Login : ComponentBase {
    private LoginVM VModel { get; } = new();

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    public JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public string ReturnUrl { get; set; } = string.Empty;

    private async Task UserLogin() {
        VModel.ReturnUrl = ReturnUrl;

        var result = await AuthenticationService.Login(VModel);

        if (result) {
            AuthStateProvider.NotifyAuthenticationStateChanged();

            if (string.IsNullOrEmpty(VModel.ReturnUrl) == false) // redirect to url
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