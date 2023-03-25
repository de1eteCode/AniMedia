using AniMedia.Domain.Models.Dtos;
using AniMedia.WebClient.Common.ApiServices;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Pages.Account;

public partial class Sessions : ComponentBase {
    private IEnumerable<SessionDto>? _sessions;

    private string? _errorMessage = null;

    [Inject]
    public IApiClient ApiClient { get; set; } = default!;

    protected override async Task OnInitializedAsync() {
        try {
            _sessions = await ApiClient.ApiV1AuthSessionsGetAsync();
        }
        catch (Exception ex) {
            _sessions = new List<SessionDto>();
            _errorMessage = ex.Message;
        }
    }
}