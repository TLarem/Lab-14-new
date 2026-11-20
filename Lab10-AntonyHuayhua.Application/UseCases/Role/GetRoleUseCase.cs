using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Role;

public class GetRoleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RoleDto> ExecuteAsync(Guid roleId)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new KeyNotFoundException($"Rol con Id '{roleId}' no encontrado.");
        }

        return role.ToDto();
    }
}
