using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.UseCases.Role;

namespace Lab10_AntonyHuayhua.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]
public class RolesController : ControllerBase
{
    private readonly CreateRoleUseCase _createRoleUseCase;
    private readonly GetRoleUseCase _getRoleUseCase;
    private readonly ListRolesUseCase _listRolesUseCase;
    private readonly UpdateRoleUseCase _updateRoleUseCase;
    private readonly DeleteRoleUseCase _deleteRoleUseCase;

    public RolesController(
        CreateRoleUseCase createRoleUseCase,
        GetRoleUseCase getRoleUseCase,
        ListRolesUseCase listRolesUseCase,
        UpdateRoleUseCase updateRoleUseCase,
        DeleteRoleUseCase deleteRoleUseCase)
    {
        _createRoleUseCase = createRoleUseCase;
        _getRoleUseCase = getRoleUseCase;
        _listRolesUseCase = listRolesUseCase;
        _updateRoleUseCase = updateRoleUseCase;
        _deleteRoleUseCase = deleteRoleUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _listRolesUseCase.ExecuteAsync();
        return Ok(roles);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var role = await _getRoleUseCase.ExecuteAsync(id);
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
    {
        var role = await _createRoleUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, role);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleDto dto)
    {
        var updated = await _updateRoleUseCase.ExecuteAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteRoleUseCase.ExecuteAsync(id);
        return NoContent();
    }
}
