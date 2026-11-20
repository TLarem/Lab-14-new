using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Ticket;

public class ChangeTicketStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public ChangeTicketStatusUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<TicketDto> ExecuteAsync(Guid ticketId, string newStatus)
    {
        var validStatuses = new[] { "abierto", "en_proceso", "cerrado" };
        var normalized = newStatus.ToLower().Trim();

        if (Array.IndexOf(validStatuses, normalized) < 0)
        {
            throw new InvalidOperationException("Estado inválido. Los estados permitidos son: 'abierto', 'en_proceso', 'cerrado'.");
        }

        var ticket = await _unitOfWork.Tickets.GetWithDetailsAsync(ticketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket con Id '{ticketId}' no encontrado.");
        }

        if (!_currentUser.IsInRole("admin") && !_currentUser.IsInRole("soporte")
            && _currentUser.UserId != ticket.UserId)
        {
            throw new System.Security.SecurityException("No tiene permisos para acceder o modificar este recurso.");
        }

        ticket.Status = normalized;
        if (normalized == "cerrado")
        {
            ticket.ClosedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
        }
        else
        {
            ticket.ClosedAt = null;
        }

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return ticket.ToDto();
    }
}
