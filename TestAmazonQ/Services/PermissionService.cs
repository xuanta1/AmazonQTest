#nullable disable
using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;

namespace TestAmazonQ.Services;

public class PermissionService
{
    private readonly AppDbContext _context;

    public PermissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UserHasPermissionAsync(string username, string permissionName)
    {
        var hasPermission = await _context.Users
            .Where(u => u.Username == username)
            .SelectMany(u => u.UserRoles)
            .SelectMany(ur => ur.Role.RolePermissions)
            .AnyAsync(rp => rp.Permission.Name == permissionName);

        return hasPermission;
    }

    public async Task<List<string>> GetUserPermissionsAsync(string username)
    {
        var permissions = await _context.Users
            .Where(u => u.Username == username)
            .SelectMany(u => u.UserRoles)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name)
            .Distinct()
            .ToListAsync();

        return permissions;
    }
}