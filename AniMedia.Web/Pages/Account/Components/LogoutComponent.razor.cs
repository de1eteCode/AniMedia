using AniMedia.Web.Contracts;
using AniMedia.Web.Providers;
using Microsoft.AspNetCore.Components;

namespace AniMedia.Web.Pages.Account.Components;

public partial class LogoutComponent : ComponentBase {

    [Parameter]
    public Func<string> GetReturnUrl { get; set; } = default!;

    [Inject]
    internal IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject]
    internal JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    public async Task LogOut() {
        await AuthenticationService.Logout();
        AuthStateProvider.NotifyAuthenticationStateChanged();

        if (GetReturnUrl != null) {
            var retUrl = GetReturnUrl.Invoke();
            if (string.IsNullOrEmpty(retUrl) == false) {
                NavigationManager.NavigateTo(retUrl);
            }
            else {
                RedirectToHome();
            }
        }
        else {
            RedirectToHome();
        }
    }

    private void RedirectToHome() {
        NavigationManager.NavigateTo("/");
    }
}