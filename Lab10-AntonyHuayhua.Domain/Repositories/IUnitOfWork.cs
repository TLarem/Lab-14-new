using System;
using System.Threading.Tasks;

namespace Lab10_AntonyHuayhua.Domain.Port.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IUserRoleRepository UserRoles { get; }
    ITicketRepository Tickets { get; }
    IResponseRepository Responses { get; }
    Task<int> SaveChangesAsync();
}
