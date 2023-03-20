namespace API.Auth.DTOs;

/// <summary>
/// User data transfer object which is returned to client
/// </summary>
public class UserDto
{
    public string DisplayName { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
    public string BudgetName { get; set; }
}