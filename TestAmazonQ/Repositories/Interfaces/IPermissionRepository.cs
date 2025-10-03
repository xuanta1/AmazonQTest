#nullable disable
using TestAmazonQ.Models;

namespace TestAmazonQ.Repositories.Interfaces;

public interface IPermissionRepository : IBaseRepository<Permission>
{
    Task<Permission> GetByNameAsync(string name);
}