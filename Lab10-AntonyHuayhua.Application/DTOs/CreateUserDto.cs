namespace Lab10_AntonyHuayhua.Application.DTOs;

public class CreateUserDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
}
