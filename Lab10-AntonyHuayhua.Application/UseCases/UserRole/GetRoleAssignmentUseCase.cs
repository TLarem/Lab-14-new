using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.UserRole;

public class GetRoleAssignmentUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleAssignmentUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserRoleDto> ExecuteAsync(Guid userId, Guid roleId)
    {
        var assignment = await _unitOfWork.UserRoles.GetAsync(userId, roleId);
        if (assignment == null)
        {
            throw new KeyNotFoundException("La asignación de rol especificada no existe.");
        }

        return assignment.ToDto();
    }
}
