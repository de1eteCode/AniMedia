using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Shared.Auth;

public partial class LoginDisplayComponent : ComponentBase {

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private string GetCurrentUrl() {
        return NavigationManager.Uri;
    }
}