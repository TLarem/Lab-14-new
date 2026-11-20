using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Role;

public class UpdateRoleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RoleDto> ExecuteAsync(Guid roleId, UpdateRoleDto dto)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new KeyNotFoundException($"Rol con Id '{roleId}' no encontrado.");
        }

        var existing = await _unitOfWork.Roles.GetByNameAsync(dto.RoleName);
        if (existing != null && existing.RoleId != roleId)
        {
            throw new InvalidOperationException($"El rol '{dto.RoleName}' ya existe.");
        }

        role.RoleName = dto.RoleName;
        _unitOfWork.Roles.Update(role);
        await _unitOfWork.SaveChangesAsync();

        return role.ToDto();
    }
}
