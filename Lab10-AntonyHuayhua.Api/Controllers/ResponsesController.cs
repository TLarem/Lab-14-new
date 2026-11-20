using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.UseCases.Response;

namespace Lab10_AntonyHuayhua.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResponsesController : ControllerBase
{
    private readonly CreateResponseUseCase _createResponseUseCase;
    private readonly GetResponseUseCase _getResponseUseCase;
    private readonly ListResponsesByTicketUseCase _listResponsesByTicketUseCase;
    private readonly UpdateResponseUseCase _updateResponseUseCase;
    private readonly DeleteResponseUseCase _deleteResponseUseCase;

    public ResponsesController(
        CreateResponseUseCase createResponseUseCase,
        GetResponseUseCase getResponseUseCase,
        ListResponsesByTicketUseCase listResponsesByTicketUseCase,
        UpdateResponseUseCase updateResponseUseCase,
        DeleteResponseUseCase deleteResponseUseCase)
    {
        _createResponseUseCase = createResponseUseCase;
        _getResponseUseCase = getResponseUseCase;
        _listResponsesByTicketUseCase = listResponsesByTicketUseCase;
        _updateResponseUseCase = updateResponseUseCase;
        _deleteResponseUseCase = deleteResponseUseCase;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _getResponseUseCase.ExecuteAsync(id);
        return Ok(response);
    }

    [HttpGet("ticket/{ticketId:guid}")]
    public async Task<IActionResult> GetByTicket(Guid ticketId)
    {
        var responses = await _listResponsesByTicketUseCase.ExecuteAsync(ticketId);
        return Ok(responses);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResponseDto dto)
    {
        var response = await _createResponseUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.ResponseId }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResponseDto dto)
    {
        var updated = await _updateResponseUseCase.ExecuteAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteResponseUseCase.ExecuteAsync(id);
        return NoContent();
    }
}
