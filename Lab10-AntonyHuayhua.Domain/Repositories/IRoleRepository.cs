using System;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Models;

namespace Lab10_AntonyHuayhua.Domain.Port.Repositories;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(string roleName);
}
