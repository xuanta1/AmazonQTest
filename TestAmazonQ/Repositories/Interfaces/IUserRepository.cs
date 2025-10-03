#nullable disable

using TestAmazonQ.Models;

namespace TestAmazonQ.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
}