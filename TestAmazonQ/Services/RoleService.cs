#nullable disable
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace TestAmazonQ.Services;

public class RoleService
{
    private readonly AppDbContext _context;

    public RoleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<bool>> AssignRoleToUserAsync(int userId, int roleId)
    {
        try
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId);

            if (!userExists || !roleExists)
                return ApiResponse<bool>.BadRequest("User hoặc Role không tồn tại");

            var existingUserRole = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (existingUserRole)
                return ApiResponse<bool>.BadRequest("User đã có role này");

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return ApiResponse<bool>.Ok(true, "Gán role thành công");
        }
        catch
        {
            return ApiResponse<bool>.InternalServerError("Lỗi khi gán role");
        }
    }

    public async Task<ApiResponse<bool>> RemoveRoleFromUserAsync(int userId, int roleId)
    {
        try
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null)
                return ApiResponse<bool>.NotFound("User không có role này");

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return ApiResponse<bool>.Ok(true, "Xóa role thành công");
        }
        catch
        {
            return ApiResponse<bool>.InternalServerError("Lỗi khi xóa role");
        }
    }
}