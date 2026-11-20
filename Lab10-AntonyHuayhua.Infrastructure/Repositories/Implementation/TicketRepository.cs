using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab10_AntonyHuayhua.Domain.Models;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;
using Lab10_AntonyHuayhua.Infrastructure.Persistence;

namespace Lab10_AntonyHuayhua.Infrastructure.Adapters.Repositories.Implementation;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(t => t.User)
            .Include(t => t.Responses)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Ticket?> GetWithDetailsAsync(Guid ticketId)
    {
        return await _dbSet
            .Include(t => t.User)
            .Include(t => t.Responses)
            .ThenInclude(r => r.Responder)
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);
    }

    public async Task<IEnumerable<Ticket>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(t => t.User)
            .Include(t => t.Responses)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
