using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Models;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Response;

public class CreateResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public CreateResponseUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<ResponseDto> ExecuteAsync(CreateResponseDto dto)
    {
        var responderId = _currentUser.UserId ?? throw new UnauthorizedAccessException("Usuario no autenticado.");

        var ticket = await _unitOfWork.Tickets.GetByIdAsync(dto.TicketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket con Id '{dto.TicketId}' no encontrado.");
        }

        if (!_currentUser.IsInRole("admin") && !_currentUser.IsInRole("soporte")
            && _currentUser.UserId != ticket.UserId)
        {
            throw new System.Security.SecurityException("No tiene permisos para acceder o modificar este recurso.");
        }

        var response = new Domain.Models.Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = dto.TicketId,
            ResponderId = responderId,
            Message = dto.Message,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
        };

        await _unitOfWork.Responses.AddAsync(response);
        await _unitOfWork.SaveChangesAsync();

        var responder = await _unitOfWork.Users.GetByIdAsync(responderId);
        response.Responder = responder!;
        return response.ToDto();
    }
}
