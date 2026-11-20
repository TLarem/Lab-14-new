using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Models;

namespace Lab10_AntonyHuayhua.Domain.Port.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetWithRolesAsync(Guid userId);
    Task<IEnumerable<User>> GetAllWithRolesAsync();
}
