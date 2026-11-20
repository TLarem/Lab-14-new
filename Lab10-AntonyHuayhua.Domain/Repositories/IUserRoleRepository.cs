using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab10_AntonyHuayhua.Domain.Models;

namespace Lab10_AntonyHuayhua.Domain.Port.Repositories;

public interface IUserRoleRepository
{
    Task<UserRole?> GetAsync(Guid userId, Guid roleId);
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task AddAsync(UserRole userRole);
    void Remove(UserRole userRole);
}
