#nullable disable
using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Repositories.Implementations;

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(AppDbContext context) : base(context) { }

    public async Task<Permission> GetByNameAsync(string name)
    {
        return await _context.Permissions.FirstOrDefaultAsync(p => p.Name == name);
    }
}