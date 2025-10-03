#nullable disable
using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Repositories.Implementations;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context) { }

    public async Task<Role> GetByNameAsync(string name)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }
}