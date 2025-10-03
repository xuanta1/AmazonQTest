#nullable disable

using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Repositories.Implementations;

/// <summary>
/// Repository for user-specific database operations
/// </summary>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Gets user by username
    /// </summary>
    /// <param name="username">Username to search for</param>
    /// <returns>User if found</returns>
    public async Task<User> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }
}