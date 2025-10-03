#nullable disable
using TestAmazonQ.Models;

namespace TestAmazonQ.Data;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Seed Permissions
        if (!context.Permissions.Any())
        {
            var permissions = new[]
            {
                new Permission { Name = "user.read", Description = "Xem thông tin user" },
                new Permission { Name = "user.create", Description = "Tạo user mới" },
                new Permission { Name = "user.update", Description = "Cập nhật user" },
                new Permission { Name = "user.delete", Description = "Xóa user" },
                new Permission { Name = "role.read", Description = "Xem thông tin role" },
                new Permission { Name = "role.create", Description = "Tạo role mới" },
                new Permission { Name = "role.update", Description = "Cập nhật role" },
                new Permission { Name = "role.delete", Description = "Xóa role" }
            };
            
            context.Permissions.AddRange(permissions);
            await context.SaveChangesAsync();
        }
        
        // Seed Roles
        if (!context.Roles.Any())
        {
            var roles = new[]
            {
                new Role { Name = "Admin", Description = "Quản trị viên hệ thống" },
                new Role { Name = "Manager", Description = "Quản lý" },
                new Role { Name = "User", Description = "Người dùng thông thường" }
            };
            
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }
        
        // Seed Role-Permission relationships
        if (!context.RolePermissions.Any())
        {
            var adminRole = context.Roles.First(r => r.Name == "Admin");
            var managerRole = context.Roles.First(r => r.Name == "Manager");
            var userRole = context.Roles.First(r => r.Name == "User");
            
            var allPermissions = context.Permissions.ToList();
            var userPermissions = allPermissions.Where(p => p.Name.StartsWith("user.")).ToList();
            
            // Admin có tất cả quyền
            foreach (var permission in allPermissions)
            {
                context.RolePermissions.Add(new RolePermission 
                { 
                    RoleId = adminRole.Id, 
                    PermissionId = permission.Id 
                });
            }
            
            // Manager có quyền user
            foreach (var permission in userPermissions)
            {
                context.RolePermissions.Add(new RolePermission 
                { 
                    RoleId = managerRole.Id, 
                    PermissionId = permission.Id 
                });
            }
            
            // User chỉ có quyền đọc
            var readPermission = allPermissions.First(p => p.Name == "user.read");
            context.RolePermissions.Add(new RolePermission 
            { 
                RoleId = userRole.Id, 
                PermissionId = readPermission.Id 
            });
            
            await context.SaveChangesAsync();
        }
    }
}