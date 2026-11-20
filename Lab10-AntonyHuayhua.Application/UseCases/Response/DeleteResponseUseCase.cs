using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Response;

public class DeleteResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public DeleteResponseUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task ExecuteAsync(Guid responseId)
    {
        var response = await _unitOfWork.Responses.GetByIdAsync(responseId);
        if (response == null)
        {
            throw new KeyNotFoundException($"Respuesta con Id '{responseId}' no encontrada.");
        }

        if (response.ResponderId != _currentUser.UserId && !_currentUser.IsInRole("admin"))
        {
            throw new System.Security.SecurityException("No tiene permisos para eliminar esta respuesta.");
        }

        _unitOfWork.Responses.Remove(response);
        await _unitOfWork.SaveChangesAsync();
    }
}
