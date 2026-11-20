namespace Lab10_AntonyHuayhua.Application.DTOs;

public class ResponseDto
{
    public Guid ResponseId { get; set; }
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string ResponderUsername { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}
