using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Role;

public class DeleteRoleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid roleId)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new KeyNotFoundException($"Rol con Id '{roleId}' no encontrado.");
        }

        _unitOfWork.Roles.Remove(role);
        await _unitOfWork.SaveChangesAsync();
    }
}
