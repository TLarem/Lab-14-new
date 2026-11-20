using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.UserRole;

public class RemoveUserRoleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserRoleUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid userId, Guid roleId)
    {
        var userRole = await _unitOfWork.UserRoles.GetAsync(userId, roleId);
        if (userRole == null)
        {
            throw new KeyNotFoundException("La asignación de rol no fue encontrada.");
        }

        _unitOfWork.UserRoles.Remove(userRole);
        await _unitOfWork.SaveChangesAsync();
    }
}
