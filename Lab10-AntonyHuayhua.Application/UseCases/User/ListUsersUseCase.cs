using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.User;

public class ListUsersUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public ListUsersUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserDto>> ExecuteAsync()
    {
        var users = await _unitOfWork.Users.GetAllWithRolesAsync();
        return users.Select(u => u.ToDto());
    }
}
