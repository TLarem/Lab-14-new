using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Application.UseCases.User;

namespace Lab10_AntonyHuayhua.Infrastructure.Adapters.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly LoginUseCase _loginUseCase;
    private readonly CreateUserUseCase _createUserUseCase;

    public AuthService(LoginUseCase loginUseCase, CreateUserUseCase createUserUseCase)
    {
        _loginUseCase = loginUseCase;
        _createUserUseCase = createUserUseCase;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        return await _loginUseCase.ExecuteAsync(request);
    }

    public async Task<UserDto> RegisterAsync(CreateUserDto request)
    {
        return await _createUserUseCase.ExecuteAsync(request);
    }
}
