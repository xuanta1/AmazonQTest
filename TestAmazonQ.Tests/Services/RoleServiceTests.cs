using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Services;
using Xunit;

namespace TestAmazonQ.Tests.Services;

public class RoleServiceTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly RoleService _roleService;

    public RoleServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new AppDbContext(options);
        _roleService = new RoleService(_context);
        
        SeedTestData();
    }

    private void SeedTestData()
    {
        var user = new User { Id = 1, Username = "testuser", PasswordHash = "hash" };
        var role = new Role { Id = 1, Name = "Admin", Description = "Administrator" };
        
        _context.Users.Add(user);
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AssignRoleToUserAsync_ReturnsSuccess_WhenValidData()
    {
        // Act
        var result = await _roleService.AssignRoleToUserAsync(1, 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Gán role thành công", result.Message);
    }

    [Fact]
    public async Task AssignRoleToUserAsync_ReturnsBadRequest_WhenUserNotExists()
    {
        // Act
        var result = await _roleService.AssignRoleToUserAsync(999, 1);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task AssignRoleToUserAsync_ReturnsBadRequest_WhenRoleNotExists()
    {
        // Act
        var result = await _roleService.AssignRoleToUserAsync(1, 999);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task AssignRoleToUserAsync_ReturnsBadRequest_WhenUserAlreadyHasRole()
    {
        // Arrange
        _context.UserRoles.Add(new UserRole { UserId = 1, RoleId = 1 });
        await _context.SaveChangesAsync();

        // Act
        var result = await _roleService.AssignRoleToUserAsync(1, 1);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("User đã có role này", result.Message);
    }

    [Fact]
    public async Task RemoveRoleFromUserAsync_ReturnsSuccess_WhenUserHasRole()
    {
        // Arrange
        _context.UserRoles.Add(new UserRole { UserId = 1, RoleId = 1 });
        await _context.SaveChangesAsync();

        // Act
        var result = await _roleService.RemoveRoleFromUserAsync(1, 1);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Xóa role thành công", result.Message);
    }

    [Fact]
    public async Task RemoveRoleFromUserAsync_ReturnsNotFound_WhenUserDoesNotHaveRole()
    {
        // Act
        var result = await _roleService.RemoveRoleFromUserAsync(1, 1);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}