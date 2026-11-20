using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.UseCases.Ticket;

namespace Lab10_AntonyHuayhua.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly CreateTicketUseCase _createTicketUseCase;
    private readonly GetTicketUseCase _getTicketUseCase;
    private readonly ListTicketsUseCase _listTicketsUseCase;
    private readonly UpdateTicketUseCase _updateTicketUseCase;
    private readonly DeleteTicketUseCase _deleteTicketUseCase;
    private readonly ChangeTicketStatusUseCase _changeTicketStatusUseCase;

    public TicketsController(
        CreateTicketUseCase createTicketUseCase,
        GetTicketUseCase getTicketUseCase,
        ListTicketsUseCase listTicketsUseCase,
        UpdateTicketUseCase updateTicketUseCase,
        DeleteTicketUseCase deleteTicketUseCase,
        ChangeTicketStatusUseCase changeTicketStatusUseCase)
    {
        _createTicketUseCase = createTicketUseCase;
        _getTicketUseCase = getTicketUseCase;
        _listTicketsUseCase = listTicketsUseCase;
        _updateTicketUseCase = updateTicketUseCase;
        _deleteTicketUseCase = deleteTicketUseCase;
        _changeTicketStatusUseCase = changeTicketStatusUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _listTicketsUseCase.ExecuteAsync();
        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ticket = await _getTicketUseCase.ExecuteAsync(id);
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
    {
        var ticket = await _createTicketUseCase.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketDto dto)
    {
        var updated = await _updateTicketUseCase.ExecuteAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteTicketUseCase.ExecuteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeTicketStatusDto dto)
    {
        var updated = await _changeTicketStatusUseCase.ExecuteAsync(id, dto.Status);
        return Ok(updated);
    }

}
