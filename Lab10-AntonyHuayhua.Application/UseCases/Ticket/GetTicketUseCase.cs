using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Ticket;

public class GetTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public GetTicketUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<TicketDto> ExecuteAsync(Guid ticketId)
    {
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

        return ticket.ToDto();
    }
}
