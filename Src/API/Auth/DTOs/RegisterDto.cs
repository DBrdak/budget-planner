using System.ComponentModel.DataAnnotations;

namespace API.Auth.DTOs;

/// <summary>
/// Register form retrived when user sign ups,
/// also validates correctness of input
/// </summary>
public class RegisterDto
{
    [Required] public string Email { get; set; }

    [Required]
    [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$", ErrorMessage = "Password must be complex")]
    public string Password { get; set; }

    [Required] public string ConfirmPassword { get; set; }

    [Required] public string Username { get; set; }

    [Required] public string DisplayName { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9]{1,16}$",
        ErrorMessage = "Max lenght is 16 and only letters and digits can be used")]
    public string BudgetName { get; set; }
}