using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab10_AntonyHuayhua.Domain.Models;
using Lab10_AntonyHuayhua.Domain.Port.Repositories;
using Lab10_AntonyHuayhua.Infrastructure.Persistence;

namespace Lab10_AntonyHuayhua.Infrastructure.Adapters.Repositories.Implementation;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(TicketeraDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.RoleName.ToLower() == roleName.ToLower());
    }
}
