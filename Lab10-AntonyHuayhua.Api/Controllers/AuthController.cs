using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;

namespace Lab10_AntonyHuayhua.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto request)
    {
        var response = await _authService.RegisterAsync(request);
        return CreatedAtAction(nameof(Register), new { id = response.UserId }, response);
    }
}
