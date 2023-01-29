using Microsoft.AspNetCore.Components;

namespace AniMedia.Web.Pages.Account.Components;

public partial class LoginDisplayComponent : ComponentBase {

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    private string GetCurrentUrl() => NavigationManager.Uri;
}