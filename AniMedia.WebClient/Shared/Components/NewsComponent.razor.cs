using Microsoft.AspNetCore.Components;

namespace AniMedia.WebClient.Shared.Components;

public partial class NewsComponent : ComponentBase {

    [Parameter]
    public string Title { get; init; } = "Title";

    [Parameter]
    public string Descriptions { get; init; } = string.Empty;

    [Parameter]
    public string HexColor { get; init; } = string.Empty;

    private string GetColorStyle() {
        if (string.IsNullOrEmpty(HexColor)) {
            return string.Empty;
        }
        else {
            if (HexColor.StartsWith("#")) {
                return $"background-color: {HexColor}";
            }
            else {
                return $"background-color: #{HexColor}";
            }
        }
    }
}