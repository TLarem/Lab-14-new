using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Response;

public class ListResponsesByTicketUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public ListResponsesByTicketUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<ResponseDto>> ExecuteAsync(Guid ticketId)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket con Id '{ticketId}' no encontrado.");
        }

        if (!_currentUser.IsInRole("admin") && !_currentUser.IsInRole("soporte")
            && _currentUser.UserId != ticket.UserId)
        {
            throw new System.Security.SecurityException("No tiene permisos para acceder o modificar este recurso.");
        }

        var responses = await _unitOfWork.Responses.GetByTicketIdWithResponderAsync(ticketId);
        return responses.Select(r => r.ToDto());
    }
}
