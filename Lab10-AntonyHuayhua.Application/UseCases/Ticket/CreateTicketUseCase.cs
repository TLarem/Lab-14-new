using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Models;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Ticket;

public class CreateTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public CreateTicketUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<TicketDto> ExecuteAsync(CreateTicketDto dto)
    {
        var userId = _currentUser.UserId ?? throw new UnauthorizedAccessException("Usuario no autenticado.");

        var ticket = new Domain.Models.Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = userId,
            Title = dto.Title,
            Description = dto.Description,
            Status = "abierto",
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        var reloaded = await _unitOfWork.Tickets.GetWithDetailsAsync(ticket.TicketId);
        return reloaded?.ToDto() ?? ticket.ToDto();
    }
}
