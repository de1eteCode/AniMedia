using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using AniMedia.WebClient.Common.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace AniMedia.WebClient.Pages.Account.Components;

[SupportedOSPlatform("browser")]
public partial class EditProfileComponent : ComponentBase {
    private readonly string _idImageAvatar = "avatarImage";
    private readonly UpdateProfileVM _vmUpdateProfile = new();
    private IBrowserFile? _newAvatar = default;

    /// <summary>
    /// Js функция установки картинки в img на странице
    /// </summary>
    /// <param name="imageElementId">Идентификатор элемента</param>
    /// <param name="imageStream">Поток картинки</param>
    [JSImport("setImage", nameof(EditProfileComponent))]
    internal static partial Task SetImage([JSMarshalAs<JSType.String>] string imageElementId, [JSMarshalAs<JSType.Any>] object imageStream);

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

    /// <summary>
    /// Загрузка нового аватара
    /// </summary>
    private async Task LoadNewAvatarEvent(InputFileChangeEventArgs e) {
        if (e.File.Size > MaxAllowedSize) {
            throw new NotImplementedException("Large file. Handle exception");
        }

        // Validate to image
        if (e.File.ContentType.StartsWith("image") == false) {
            return;
        }

        _newAvatar = e.File;

        // set preview image profile

        var ms = new MemoryStream();

        await _newAvatar.OpenReadStream(MaxAllowedSize).CopyToAsync(ms);

        ms.Seek(0, SeekOrigin.Begin);
        
        var dotnetImageStream = new DotNetStreamReference(ms);

        await SetImage(_idImageAvatar, dotnetImageStream);
    }
}