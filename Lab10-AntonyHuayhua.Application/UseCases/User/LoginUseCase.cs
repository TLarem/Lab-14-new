using System.Linq;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.User;

public class LoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUnitOfWork unitOfWork, IPasswordService passwordService, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> ExecuteAsync(LoginRequestDto dto)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
        }

        var isValid = _passwordService.VerifyPassword(dto.Password, user.PasswordHash);
        if (!isValid)
        {
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");
        }

        var roles = user.UserRoles?.Select(ur => ur.Role?.RoleName).Where(r => r != null).Select(r => r!).ToList() ?? new();
        var token = _tokenService.GenerateToken(user, roles);

        return new AuthResponseDto
        {
            Token = token,
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            Roles = roles
        };
    }
}
