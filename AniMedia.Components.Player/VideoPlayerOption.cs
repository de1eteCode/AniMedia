namespace AniMedia.Components.Player;

public class VideoPlayerOption {

    /// <summary>
    /// 宽度
    /// </summary>
    public int Width { get; set; } = 300;

    /// <summary>
    /// 高度
    /// </summary>
    public int Height { get; set; } = 200;

    /// <summary>
    /// 显示控制条,默认 true
    /// </summary>
    public bool Controls { get; set; } = true;

    /// <summary>
    /// 自动播放,默认 true
    /// </summary>
    public bool Autoplay { get; set; } = true;

    /// <summary>
    /// 预载,默认 auto
    /// </summary>
    public string Preload { get; set; } = "auto";

    /// <summary>
    /// 播放资源
    /// </summary>
    public List<VideoSources> Sources { get; set; } = new List<VideoSources>();

    /// <summary>
    /// 设置封面资源,相对或者绝对路径
    /// </summary>
    public string? Poster { get; set; }
}