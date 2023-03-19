using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User : IdentityUser
{
    [Required]
    public string DisplayName { get; set; }
}