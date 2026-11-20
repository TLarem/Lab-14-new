using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.User;

public class GetUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> ExecuteAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetWithRolesAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con Id '{userId}' no encontrado.");
        }

        return user.ToDto();
    }
}