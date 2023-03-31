using System.ComponentModel.DataAnnotations;

namespace AniMedia.WebClient.Common.ViewModels;

public class LoginVM {

    [Required]
    public string Nickname { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;
}