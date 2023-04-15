using System.ComponentModel.DataAnnotations;

namespace AniMedia.WebClient.Common.ViewModels; 

public class UpdateProfileVM {

    [Required]
    [StringLength(25, MinimumLength = 0, ErrorMessage = "Lenght: 0-25")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(25, MinimumLength = 0, ErrorMessage = "Lenght: 0-25")]
    public string SecondName { get; set; } = string.Empty;
}