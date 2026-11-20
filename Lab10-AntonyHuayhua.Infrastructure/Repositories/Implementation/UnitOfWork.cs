using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;
using Lab10_AntonyHuayhua.Infrastructure.Persistence;

namespace Lab10_AntonyHuayhua.Infrastructure.Adapters.Repositories.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly TicketeraDbContext _context;
    private IUserRepository? _users;
    private IRoleRepository? _roles;
    private IUserRoleRepository? _userRoles;
    private ITicketRepository? _tickets;
    private IResponseRepository? _responses;

    public UnitOfWork(TicketeraDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
    public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(_context);
    public ITicketRepository Tickets => _tickets ??= new TicketRepository(_context);
    public IResponseRepository Responses => _responses ??= new ResponseRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
