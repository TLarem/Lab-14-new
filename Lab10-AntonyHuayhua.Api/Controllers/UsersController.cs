using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.UseCases.User;

namespace Lab10_AntonyHuayhua.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly GetUserUseCase _getUserUseCase;
    private readonly ListUsersUseCase _listUsersUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;

    public UsersController(
        GetUserUseCase getUserUseCase,
        ListUsersUseCase listUsersUseCase,
        UpdateUserUseCase updateUserUseCase,
        DeleteUserUseCase deleteUserUseCase)
    {
        _getUserUseCase = getUserUseCase;
        _listUsersUseCase = listUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _listUsersUseCase.ExecuteAsync();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _getUserUseCase.ExecuteAsync(id);
        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var updated = await _updateUserUseCase.ExecuteAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteUserUseCase.ExecuteAsync(id);
        return NoContent();
    }

}
