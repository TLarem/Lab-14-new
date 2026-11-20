using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Models;

namespace Lab10_AntonyHuayhua.Domain.Port.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId);
    Task<Ticket?> GetWithDetailsAsync(Guid ticketId);
    Task<IEnumerable<Ticket>> GetAllWithDetailsAsync();
}
