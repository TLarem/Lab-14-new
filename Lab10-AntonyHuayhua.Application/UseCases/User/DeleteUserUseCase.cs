using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.User;

public class DeleteUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con Id '{userId}' no encontrado.");
        }

        _unitOfWork.Users.Remove(user);
        await _unitOfWork.SaveChangesAsync();
    }
}
