namespace API.Auth.DTOs;

/// <summary>
/// Login form retrived when user logs in
/// </summary>

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}