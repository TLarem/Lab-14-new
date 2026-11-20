using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Models;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.User;

public class CreateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;

    public CreateUserUseCase(IUnitOfWork unitOfWork, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
    }

    public async Task<UserDto> ExecuteAsync(CreateUserDto dto)
    {
        var existing = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
        if (existing != null)
        {
            throw new InvalidOperationException($"El nombre de usuario '{dto.Username}' ya está en uso.");
        }

        if (!string.IsNullOrEmpty(dto.Email))
        {
            var existingEmail = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (existingEmail != null)
            {
                throw new InvalidOperationException($"El correo '{dto.Email}' ya está registrado.");
            }
        }

        var user = new Domain.Models.User
        {
            UserId = Guid.NewGuid(),
            Username = dto.Username,
            PasswordHash = _passwordService.HashPassword(dto.Password),
            Email = dto.Email,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
        };

        await _unitOfWork.Users.AddAsync(user);

        var defaultRole = await _unitOfWork.Roles.GetByNameAsync("usuario");
        if (defaultRole != null)
        {
            await _unitOfWork.UserRoles.AddAsync(new Domain.Models.UserRole
            {
                UserId = user.UserId,
                RoleId = defaultRole.RoleId,
                AssignedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            });
        }

        await _unitOfWork.SaveChangesAsync();
        return user.ToDto();
    }
}
