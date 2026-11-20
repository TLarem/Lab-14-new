using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.User;

public class UpdateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> ExecuteAsync(Guid userId, UpdateUserDto dto)
    {
        var user = await _unitOfWork.Users.GetWithRolesAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con Id '{userId}' no encontrado.");
        }

        var existingUser = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
        if (existingUser != null && existingUser.UserId != userId)
        {
            throw new InvalidOperationException($"El nombre de usuario '{dto.Username}' ya está en uso.");
        }

        if (!string.IsNullOrEmpty(dto.Email))
        {
            var existingEmail = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (existingEmail != null && existingEmail.UserId != userId)
            {
                throw new InvalidOperationException($"El correo '{dto.Email}' ya está registrado.");
            }
        }

        user.Username = dto.Username;
        user.Email = dto.Email;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return user.ToDto();
    }
}
