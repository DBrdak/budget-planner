using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public class PasswordFormDto
{
    [Required]
    [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$", ErrorMessage = "Password must be complex")]
    public string NewPassword { get; set; }

    public string OldPassword { get; set; }
}