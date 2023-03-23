using System.ComponentModel.DataAnnotations;

namespace AniMedia.Web.Models.ViewModels.Identity;

public class RegisterVM {

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string SecondName { get; set; } = string.Empty;
}