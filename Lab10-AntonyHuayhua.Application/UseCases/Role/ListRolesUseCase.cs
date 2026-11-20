using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Role;

public class ListRolesUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public ListRolesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RoleDto>> ExecuteAsync()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        return roles.Select(r => r.ToDto());
    }
}
