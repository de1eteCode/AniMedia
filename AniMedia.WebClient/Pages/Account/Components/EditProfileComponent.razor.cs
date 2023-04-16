using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using AniMedia.Domain.Interfaces;
using AniMedia.Domain.Models.Requests;
using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.Models;
using AniMedia.WebClient.Common.Store;
using AniMedia.WebClient.Common.ViewModels;
using Blazored.Toast.Services;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace AniMedia.WebClient.Pages.Account.Components;

public partial class EditProfileComponent : FluxorComponent {
    private readonly UpdateProfileVM _vmUpdateProfile = new();
    private IBrowserFile? _newAvatar = default;
    
    //https://github.com/SteveSandersonMS/BlazorInputFile/issues/2
    private bool DoubleBuffer { get; set; }

    [Inject]
    public IApiUrlBuilder ApiUrlBuilder { get; init; } = default!;
    
    [Inject]
    public IApiClient ApiClient { get; init; } = default!;

    [Inject]
    public IState<UserInfoState> UserState { get; init; } = default!;

    [Inject]
    public IDispatcher Dispatcher { get; init; } = default!;

    [Inject]
    public IToastService ToastService { get; init; } = default!;

    /// <summary>
    /// Обновления профиля, текстового содержания
    /// </summary>
    private async Task UpdateProfile() {
        // todo: track change

        var res = await ApiClient.ApiV1AccountUpdateAsync(new UpdateProfileRequest() {
            FirstName = _vmUpdateProfile.FirstName,
            SecondName = _vmUpdateProfile.SecondName
        });
    }
}