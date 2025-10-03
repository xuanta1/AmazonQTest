#nullable disable
using TestAmazonQ.Data;
using TestAmazonQ.Models;
using TestAmazonQ.Models.Responses;
using TestAmazonQ.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TestAmazonQ.Services;

public class RoleService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public RoleService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<ApiResponse<bool>> AssignRoleToUserAsync(int userId, int roleId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (user == null || role == null)
                return ApiResponse<bool>.BadRequest("User hoặc Role không tồn tại");

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
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return ApiResponse<bool>.NotFound("User không có role này");

            return ApiResponse<bool>.Ok(true, "Xóa role thành công");
        }
        catch
        {
            return ApiResponse<bool>.InternalServerError("Lỗi khi xóa role");
        }
    }
}