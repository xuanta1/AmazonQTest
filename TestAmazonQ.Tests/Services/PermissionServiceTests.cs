using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Services;
using Xunit;

namespace TestAmazonQ.Tests.Services;

public class PermissionServiceTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly PermissionService _permissionService;

    public PermissionServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new AppDbContext(options);
        _permissionService = new PermissionService(_context);
        
        SeedTestData();
    }

    private void SeedTestData()
    {
        var user = new User { Id = 1, Username = "testuser", PasswordHash = "hash" };
        var role = new Role { Id = 1, Name = "Admin", Description = "Administrator" };
        var permission = new Permission { Id = 1, Name = "user.read", Description = "Read users" };
        
        _context.Users.Add(user);
        _context.Roles.Add(role);
        _context.Permissions.Add(permission);
        _context.UserRoles.Add(new UserRole { UserId = 1, RoleId = 1 });
        _context.RolePermissions.Add(new RolePermission { RoleId = 1, PermissionId = 1 });
        
        _context.SaveChanges();
    }

    [Fact]
    public async Task UserHasPermissionAsync_ReturnsTrue_WhenUserHasPermission()
    {
        // Act
        var result = await _permissionService.UserHasPermissionAsync("testuser", "user.read");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UserHasPermissionAsync_ReturnsFalse_WhenUserDoesNotHavePermission()
    {
        // Act
        var result = await _permissionService.UserHasPermissionAsync("testuser", "user.delete");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UserHasPermissionAsync_ReturnsFalse_WhenUserNotExists()
    {
        // Act
        var result = await _permissionService.UserHasPermissionAsync("nonexistent", "user.read");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetUserPermissionsAsync_ReturnsPermissions_WhenUserExists()
    {
        // Act
        var permissions = await _permissionService.GetUserPermissionsAsync("testuser");

        // Assert
        Assert.Single(permissions);
        Assert.Contains("user.read", permissions);
    }

    [Fact]
    public async Task GetUserPermissionsAsync_ReturnsEmpty_WhenUserNotExists()
    {
        // Act
        var permissions = await _permissionService.GetUserPermissionsAsync("nonexistent");

        // Assert
        Assert.Empty(permissions);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}