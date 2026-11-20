using System.Linq;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Models;

namespace Lab10_AntonyHuayhua.Application.DTOs;

public static class DtoMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            Roles = user.UserRoles?.Select(ur => ur.Role?.RoleName).Where(r => r != null).Select(r => r!).ToList() ?? new()
        };
    }

    public static RoleDto ToDto(this Role role)
    {
        return new RoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };
    }

    public static UserRoleDto ToDto(this UserRole ur)
    {
        return new UserRoleDto
        {
            UserId = ur.UserId,
            Username = ur.User?.Username ?? string.Empty,
            RoleId = ur.RoleId,
            RoleName = ur.Role?.RoleName ?? string.Empty,
            AssignedAt = ur.AssignedAt
        };
    }

    public static TicketDto ToDto(this Ticket ticket)
    {
        return new TicketDto
        {
            TicketId = ticket.TicketId,
            UserId = ticket.UserId,
            Username = ticket.User?.Username ?? string.Empty,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt,
            ClosedAt = ticket.ClosedAt,
            ResponseCount = ticket.Responses?.Count ?? 0
        };
    }

    public static ResponseDto ToDto(this Response response)
    {
        return new ResponseDto
        {
            ResponseId = response.ResponseId,
            TicketId = response.TicketId,
            ResponderId = response.ResponderId,
            ResponderUsername = response.Responder?.Username ?? string.Empty,
            Message = response.Message,
            CreatedAt = response.CreatedAt
        };
    }
}
