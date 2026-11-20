using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;

namespace Lab10_AntonyHuayhua.Application.Port.Services;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<UserDto> RegisterAsync(CreateUserDto request);
}
