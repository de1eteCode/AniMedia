using AniMedia.Domain.Models.Dtos;
using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Pages.Account.Components;

[Authorize]
public partial class SessionsComponent : ComponentBase {
    private string? _errorMessage;
    private ICollection<SessionDto>? _sessions;
    private SessionDto? _currentSession;

    [Inject]
    public IApiClient ApiClient { get; set; } = default!;

    [Inject]
    public JwtAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    public ITokenService TokenService { get; set; } = default!;

    protected override async Task OnInitializedAsync() {
        try {
            _currentSession = await ApiClient.ApiV1SessionAsync(await TokenService.GetTokenAsync());

            // Todo: Paging change on table
            _sessions = (await ApiClient.ApiV1SessionListAsync(1, 100)).Items.ToList();
        }
        catch (Exception ex) {
            _sessions = new List<SessionDto>();
            _errorMessage = ex.Message;
        }
    }

    private async Task CloseSession(Guid sessionUid) {
        if ((_sessions ?? Enumerable.Empty<SessionDto>()).Any(e => e.Uid == sessionUid) == false) {
            return;
        }

        var res = await ApiClient.ApiV1SessionRemoveAsync(sessionUid);

        if (res.Uid.Equals(sessionUid)) {
            _sessions!.Remove(_sessions.First(e => e.Uid.Equals(sessionUid)));

            // Todo: Придумать что-нить получше
            AuthStateProvider.NotifyAuthenticationStateChanged();
        }
    }
}