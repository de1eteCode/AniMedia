namespace AniMedia.Web.Models.ViewModels;

internal class ErrorVM {
    public string RequestId { get; set; } = string.Empty;

    public bool ShowRequestId => string.IsNullOrEmpty(RequestId) == false;
}