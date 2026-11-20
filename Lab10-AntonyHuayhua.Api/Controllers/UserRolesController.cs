using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.UseCases.UserRole;

namespace Lab10_AntonyHuayhua.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]
public class UserRolesController : ControllerBase
{
    private readonly AssignUserRoleUseCase _assignUserRoleUseCase;
    private readonly RemoveUserRoleUseCase _removeUserRoleUseCase;
    private readonly GetRoleAssignmentUseCase _getRoleAssignmentUseCase;
    private readonly ListRoleAssignmentsUseCase _listRoleAssignmentsUseCase;

    public UserRolesController(
        AssignUserRoleUseCase assignUserRoleUseCase,
        RemoveUserRoleUseCase removeUserRoleUseCase,
        GetRoleAssignmentUseCase getRoleAssignmentUseCase,
        ListRoleAssignmentsUseCase listRoleAssignmentsUseCase)
    {
        _assignUserRoleUseCase = assignUserRoleUseCase;
        _removeUserRoleUseCase = removeUserRoleUseCase;
        _getRoleAssignmentUseCase = getRoleAssignmentUseCase;
        _listRoleAssignmentsUseCase = listRoleAssignmentsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assignments = await _listRoleAssignmentsUseCase.ExecuteAsync();
        return Ok(assignments);
    }

    [HttpGet("{userId:guid}/{roleId:guid}")]
    public async Task<IActionResult> Get(Guid userId, Guid roleId)
    {
        var assignment = await _getRoleAssignmentUseCase.ExecuteAsync(userId, roleId);
        return Ok(assignment);
    }

    [HttpPost]
    public async Task<IActionResult> Assign([FromBody] AssignUserRoleDto dto)
    {
        var assigned = await _assignUserRoleUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(Get), new { userId = assigned.UserId, roleId = assigned.RoleId }, assigned);
    }

    [HttpDelete("{userId:guid}/{roleId:guid}")]
    public async Task<IActionResult> Remove(Guid userId, Guid roleId)
    {
        await _removeUserRoleUseCase.ExecuteAsync(userId, roleId);
        return NoContent();
    }

}
