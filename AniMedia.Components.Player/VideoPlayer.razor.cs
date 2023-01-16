using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace AniMedia.Components.Player;

public partial class VideoPlayer : IAsyncDisposable {

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    private DotNetObjectReference<VideoPlayer>? Instance { get; set; }

    private ElementReference Element { get; set; }

    private bool IsInitialized { get; set; }

    private string? DebugInfo { get; set; }

    [NotNull]
    private string? Id { get; set; }

    /// <summary>
    /// Адрес ресурса
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public string? Url { get; set; }

    /// <summary>
    /// Тип ресурса
    /// <para>video/mp4</para>
    /// <para>application/x-mpegURL</para>
    /// <para>video/ogg</para>
    /// <para>video/x-matroska</para>
    /// <para>Обратитесь к <see cref="EnumVideoType"/> для получения дополнительной информации</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MineType { get; set; } = "application/x-mpegURL";

    /// <summary>
    /// Ширина
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 300;

    /// <summary>
    /// Высота
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 200;

    /// <summary>
    /// Отобразите панель управления, значение по умолчанию: true
    /// </summary>
    [Parameter]
    public bool Controls { get; set; } = true;

    /// <summary>
    /// Автоматическое воспроизведение, значение по умолчанию: false
    /// </summary>
    [Parameter]
    public bool Autoplay { get; set; } = false;

    /// <summary>
    /// Предварительная загрузка, по умолчанию: auto
    /// </summary>
    [Parameter]
    public string Preload { get; set; } = "auto";

    /// <summary>
    /// Задайте ресурс обложки, относительный или абсолютный путь
    /// </summary>
    [Parameter]
    public string? Poster { get; set; }

    /// <summary>
    /// Отображение отладочной информации
    /// </summary>
    [Parameter]
    public bool Debug { get; set; }

    /// <summary>
    /// Метод обратного вызова Get/set error
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized() {
        base.OnInitialized();

        Id = $"vp_{GetHashCode()}";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if (firstRender) {
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{GetProjectName()}/videoplayerapp.js");
            Instance = DotNetObjectReference.Create(this);
            await MakesurePlayerReady();
        }
    }

    protected string GetProjectName() => GetType().Assembly.FullName!.Split(',').First();

    /// <summary>
    /// Инициализация, никакие юридические параметры Url не инициализированы, перезагрузка обнаружит и повторно инициализирует
    /// </summary>
    /// <returns></returns>
    private async Task MakesurePlayerReady() {
        if (!IsInitialized) {
            if (string.IsNullOrEmpty(Url)) {
                await Logger($"Url is empty");
            }
            else {
                var option = new VideoPlayerOption() {
                    Width = Width,
                    Height = Height,
                    Controls = Controls,
                    Autoplay = Autoplay,
                    Preload = Preload,
                    Poster = Poster
                };
                option.Sources.Add(new VideoSources(MineType, Url));
                await Module.InvokeVoidAsync("loadPlayer", Instance, Id, option);
            }
        }
    }

    /// <summary>
    /// Переключение ресурсов воспроизведения
    /// </summary>
    /// <param name="url"></param>
    /// <param name="mineType"></param>
    /// <returns></returns>
    public virtual async Task Reload(string url, string mineType) {
        Url = url;
        MineType = mineType;
        await MakesurePlayerReady();
        await Module.InvokeVoidAsync("reloadPlayer", url, mineType);
    }

    /// <summary>
    /// Установите крышку
    /// </summary>
    /// <param name="poster"></param>
    /// <returns></returns>
    public virtual async Task SetPoster(string poster) {
        Poster = poster;
        await Module.InvokeVoidAsync("setPoster", poster);
    }

    /// <summary>
    /// Метод обратного вызова JS
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public void GetInit() => IsInitialized = true;

    /// <summary>
    /// Метод обратного вызова JS
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task Logger(string message) {
        DebugInfo = message;
        if (Debug) {
            StateHasChanged();
        }

        Console.WriteLine(DebugInfo);
        if (OnError != null) {
            await OnError.Invoke(DebugInfo);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync() {
        if (Module is not null) {
            await Module.InvokeVoidAsync("destroy", Id);
            await Module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}