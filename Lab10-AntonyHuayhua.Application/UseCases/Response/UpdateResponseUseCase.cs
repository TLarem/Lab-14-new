using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Response;

public class UpdateResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public UpdateResponseUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<ResponseDto> ExecuteAsync(Guid responseId, UpdateResponseDto dto)
    {
        var response = await _unitOfWork.Responses.GetByIdAsync(responseId);
        if (response == null)
        {
            throw new KeyNotFoundException($"Respuesta con Id '{responseId}' no encontrada.");
        }

        if (response.ResponderId != _currentUser.UserId && !_currentUser.IsInRole("admin"))
        {
            throw new System.Security.SecurityException("No tiene permisos para editar esta respuesta.");
        }

        response.Message = dto.Message;
        _unitOfWork.Responses.Update(response);
        await _unitOfWork.SaveChangesAsync();

        var responder = await _unitOfWork.Users.GetByIdAsync(response.ResponderId);
        response.Responder = responder!;

        return response.ToDto();
    }
}
