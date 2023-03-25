using System.ComponentModel.DataAnnotations;

namespace AniMedia.WebClient.Common.ViewModels;

public class RegistrationVM {

    [Required]
    [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "Length: 3 - 20")]
    public string Nickname { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Lenght: 8 - 50")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "Lenght: 8 - 50")]
    [Compare(nameof(Password), ErrorMessage = "Not equal to password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}