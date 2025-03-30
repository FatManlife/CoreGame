namespace Api.Models.DTOs;

public class RegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Hashed_password { get; set; }
    public DateTime? Date_created { get; set; }
}