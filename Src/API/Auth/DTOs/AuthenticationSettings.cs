namespace API.AuthDTO;

/// <summary>
/// Current authentication setting specified in json file
/// </summary>

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; }
}