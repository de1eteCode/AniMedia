using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using AniMedia.Domain.Interfaces;
using AniMedia.WebClient.Common.Contracts;
using AniMedia.WebClient.Common.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace AniMedia.WebClient.Pages.Account.Components;

public partial class EditProfileComponent : ComponentBase {
    private readonly string _idImageAvatar = "avatarImage";
    private readonly UpdateProfileVM _vmUpdateProfile = new();
    private IBrowserFile? _newAvatar = default;
    private string? _newAvatarB64 = default;

    [Inject]
    public IJSRuntime JsRuntime { get; init; } = default!;

    /// <summary>
    /// Загружен ли новый аватар пользователем
    /// </summary>
    private bool AvatarIsLoaded {
        get {
            return _newAvatar is not null;
        }
    }

    /// <summary>
    /// Максимальный размер загружаемого изображения
    /// </summary>
    // Todo: Изменить хардкод на конфигурацию
    private const int MaxSizeImageUploadMB = 2;

    private long MaxAllowedSize => MaxSizeImageUploadMB * 1048576; // 1024*1024

    /// <summary>
    /// Обновления профиля, текстового содержания
    /// </summary>
    private Task UpdateProfile() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Обновление автара профиля
    /// </summary>
    private Task UpdateAvatar() {
        throw new NotImplementedException();
    }

    private string GetImageInBase64() {
        if (string.IsNullOrEmpty(_newAvatarB64) || _newAvatar == null) {
            return string.Empty;
        }
        
        return $"data:{_newAvatar.ContentType};base64, {_newAvatarB64}";
    }
    
    /// <summary>
    /// Загрузка нового аватара
    /// </summary>
    private async Task LoadNewAvatarEvent(InputFileChangeEventArgs e) {
        if (e.File.Size > MaxAllowedSize) {
            throw new NotImplementedException("Large file. Handle exception");
        }

        if (e.File.ContentType.StartsWith("image") && MimeKit.MimeTypes.TryGetExtension(e.File.ContentType, out _)) {
            _newAvatar = e.File;
            
            await using var ms = new MemoryStream();
            await _newAvatar.OpenReadStream(MaxAllowedSize).CopyToAsync(ms);
            _newAvatarB64 = Convert.ToBase64String(ms.ToArray());
        }
    }
}