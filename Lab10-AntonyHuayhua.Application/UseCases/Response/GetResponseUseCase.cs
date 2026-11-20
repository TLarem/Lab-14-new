using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Response;

public class GetResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public GetResponseUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<ResponseDto> ExecuteAsync(Guid responseId)
    {
        var response = await _unitOfWork.Responses.GetByIdAsync(responseId);
        if (response == null)
        {
            throw new KeyNotFoundException($"Respuesta con Id '{responseId}' no encontrada.");
        }

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(response.TicketId);
        if (ticket != null)
        {
            if (!_currentUser.IsInRole("admin") && !_currentUser.IsInRole("soporte")
            && _currentUser.UserId != ticket.UserId)
        {
            throw new System.Security.SecurityException("No tiene permisos para acceder o modificar este recurso.");
        }
        }

        var responder = await _unitOfWork.Users.GetByIdAsync(response.ResponderId);
        response.Responder = responder!;

        return response.ToDto();
    }
}
