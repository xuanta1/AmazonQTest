#nullable disable
using TestAmazonQ.Models;

namespace TestAmazonQ.Repositories.Interfaces;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role> GetByNameAsync(string name);
}