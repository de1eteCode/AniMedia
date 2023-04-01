﻿using AniMedia.Domain.Models.Dtos;
using AniMedia.WebClient.Common.ApiServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Pages.Account;

[Authorize]
public partial class SessionsPage : ComponentBase {
    private string? _errorMessage;
    private IEnumerable<SessionDto>? _sessions;

    [Inject]
    public IApiClient ApiClient { get; set; } = default!;

    protected override async Task OnInitializedAsync() {
        try {
            _sessions = await ApiClient.ApiV1SessionListAsync();
        }
        catch (Exception ex) {
            _sessions = new List<SessionDto>();
            _errorMessage = ex.Message;
        }
    }
}