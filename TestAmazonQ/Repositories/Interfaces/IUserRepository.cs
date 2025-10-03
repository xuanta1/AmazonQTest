#nullable disable

using TestAmazonQ.Models;

namespace TestAmazonQ.Repositories.Interfaces;

/// <summary>
/// Repository interface for user-specific operations
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Gets user by username
    /// </summary>
    /// <param name="username">Username to search for</param>
    /// <returns>User if found</returns>
    Task<User> GetByUsernameAsync(string username);
}