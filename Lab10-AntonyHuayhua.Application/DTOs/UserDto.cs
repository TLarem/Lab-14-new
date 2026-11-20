namespace Lab10_AntonyHuayhua.Application.DTOs;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
