namespace Lab10_AntonyHuayhua.Application.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
