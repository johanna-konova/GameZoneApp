using System.ComponentModel.DataAnnotations;

using static GameZoneApp.Core.Constants.ModelsConstants;
using static GameZoneApp.Core.Constants.ModelsMessagesConstants;

namespace GameZoneApp.Core.Models.User
{
    public class RegisterFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [StringLength(PasswordMaxLength,
            MinimumLength = PasswordMinLength,
            ErrorMessage = InvalidPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; init; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = MismatchedPasswords)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
