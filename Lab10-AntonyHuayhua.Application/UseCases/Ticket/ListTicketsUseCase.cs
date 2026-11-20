using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Application.DTOs;
using Lab10_AntonyHuayhua.Application.Port.Services;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;

namespace Lab10_AntonyHuayhua.Application.UseCases.Ticket;

public class ListTicketsUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public ListTicketsUseCase(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<TicketDto>> ExecuteAsync()
    {
        if (_currentUser.IsInRole("admin") || _currentUser.IsInRole("soporte"))
        {
            var tickets = await _unitOfWork.Tickets.GetAllWithDetailsAsync();
            return tickets.Select(t => t.ToDto());
        }

        var userId = _currentUser.UserId;
        if (userId.HasValue)
        {
            var userTickets = await _unitOfWork.Tickets.GetByUserIdAsync(userId.Value);
            return userTickets.Select(t => t.ToDto());
        }

        return Enumerable.Empty<TicketDto>();
    }
}
