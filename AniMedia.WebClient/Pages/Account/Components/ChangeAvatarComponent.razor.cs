using AniMedia.WebClient.Common.ApiServices;
using AniMedia.WebClient.Common.Models;
using AniMedia.WebClient.Common.Store;
using Blazored.Toast.Services;
using Blazorise.Bootstrap5;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;

namespace AniMedia.WebClient.Pages.Account.Components; 

public partial class ChangeAvatarComponent : ComponentBase {

    private Modal _editAvatarRef;
    private IBrowserFile? _newAvatar = default;
    
    //https://github.com/SteveSandersonMS/BlazorInputFile/issues/2
    private bool DoubleBuffer { get; set; }
    
    [Inject]
    public IApiClient ApiClient { get; init; } = default!;

    [Inject]
    public IOptionsMonitor<UploadSettings> Settings { get; init; } = default!;
    
    [Inject]
    public IState<UserInfoState> UserState { get; init; } = default!;
    
    [Inject]
    public IToastService ToastService { get; init; } = default!;
    
    [Inject]
    public IDispatcher Dispatcher { get; init; } = default!;

    private Task Show() {
        return _editAvatarRef.Show();
    }

    private Task Hide() {
        return _editAvatarRef.Hide();
    }
    
    /// <summary>
    /// Обновление автара профиля
    /// </summary>
    private async Task UpdateAvatar() {
        if (_newAvatar == null) {
            return;
        }

        var allowedSize = UploadSettings.ConvertMbToBytes(Settings.CurrentValue.MaxImageSizeMb);
        var result = await ApiClient.ApiV1AccountUpdateavatarAsync(new FileParameter(
            _newAvatar.OpenReadStream(allowedSize),
            _newAvatar.Name,
            _newAvatar.ContentType));

        if (result != null) {
            var action = new UserInfoActions.SetNewAvatar {
                Uid = result.UID
            };

            Dispatcher.Dispatch(action);

            // убрать выбранный файл
            _newAvatar = null;
            DoubleBuffer = !DoubleBuffer;

            await Hide();
            ToastService.ShowInfo("Аватар успешно обновлен");
        }
    }
    
    /// <summary>
    /// Загрузка нового аватара
    /// </summary>
    private void LoadNewAvatarEvent(InputFileChangeEventArgs e) {
        var allowedSize = UploadSettings.ConvertMbToBytes(Settings.CurrentValue.MaxImageSizeMb);

        if (e.File.Size > allowedSize) {
            ToastService.ShowError($"Размер файла не может превышать {Settings.CurrentValue.MaxImageSizeMb} МБ");
            _newAvatar = null;
            DoubleBuffer = !DoubleBuffer;
            return;
        }

        if (e.File.ContentType.StartsWith("image") == false) {
            ToastService.ShowError($"Выбранный файл не поддерживается");
            _newAvatar = null;
            DoubleBuffer = !DoubleBuffer;
            return;
        }

        _newAvatar = e.File;
    }
}