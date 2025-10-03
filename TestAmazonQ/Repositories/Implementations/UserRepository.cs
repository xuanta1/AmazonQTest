#nullable disable

using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }
}