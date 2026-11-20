using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.UserRole;

public class AssignUserRoleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserRoleUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserRoleDto> ExecuteAsync(AssignUserRoleDto dto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con Id '{dto.UserId}' no encontrado.");
        }

        var role = await _unitOfWork.Roles.GetByIdAsync(dto.RoleId);
        if (role == null)
        {
            throw new KeyNotFoundException($"Rol con Id '{dto.RoleId}' no encontrado.");
        }

        var existing = await _unitOfWork.UserRoles.GetAsync(dto.UserId, dto.RoleId);
        if (existing != null)
        {
            throw new InvalidOperationException("El usuario ya tiene asignado este rol.");
        }

        var userRole = new Domain.Models.UserRole
        {
            UserId = dto.UserId,
            RoleId = dto.RoleId,
            AssignedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            User = user,
            Role = role
        };

        await _unitOfWork.UserRoles.AddAsync(userRole);
        await _unitOfWork.SaveChangesAsync();

        return userRole.ToDto();
    }
}
