using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Providers;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Shared.Auth;

public partial class LogoutComponent : ComponentBase {

    [Parameter] public Func<string> GetReturnUrl { get; set; } = default!;

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Inject] internal IAuthenticationService AuthenticationService { get; set; } = default!;

    [Inject] internal JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    public async Task LogOut() {
        await AuthenticationService.Logout();
        AuthStateProvider.NotifyAuthenticationStateChanged();

        if (GetReturnUrl != null) {
            var retUrl = GetReturnUrl.Invoke();
            if (string.IsNullOrEmpty(retUrl) == false)
                NavigationManager.NavigateTo(retUrl);
            else
                RedirectToHome();
        }
        else {
            RedirectToHome();
        }
    }

    private void RedirectToHome() {
        NavigationManager.NavigateTo("/");
    }
}