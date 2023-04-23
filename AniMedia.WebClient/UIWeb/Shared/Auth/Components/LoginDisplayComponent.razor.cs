using AniMedia.WebClient.Common.Providers;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.UIWeb.Shared.Auth.Components;

public partial class LoginDisplayComponent : ComponentBase {

    [Parameter]
    public Func<string> GetReturnUrl { get; set; } = default!;

    [Inject]
    internal JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private string GetCurrentUrl() {
        return NavigationManager.Uri;
    }
    private async Task LogOut() {
        await AuthStateProvider.Logout();

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