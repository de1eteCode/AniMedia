using AniMedia.Web.Contracts;
using AniMedia.Web.Models.ViewModels.Identity;
using AniMedia.Web.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.Web.Pages.Account;

[AllowAnonymous]
public partial class Login : ComponentBase {
    private LoginVM VModel { get; } = new LoginVM();

    [Parameter]
    public string ReturnUrl { get; set; } = string.Empty;

    [Inject]
    internal IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    internal JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private async Task Authenticate() {
        VModel.ReturnUrl = ReturnUrl;

        var result = await AuthenticationService.Authenticate(VModel);

        if (result) {
            AuthStateProvider.NotifyAuthenticationStateChanged();

            if (string.IsNullOrEmpty(VModel.ReturnUrl) == false) {
                // redirect to url
                NavigationManager.NavigateTo(ReturnUrl);
            }
            else {
                // redirect to home
                NavigationManager.NavigateTo("/");
            }
        }
    }
}