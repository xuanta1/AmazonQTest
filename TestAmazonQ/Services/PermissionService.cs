#nullable disable
using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Data;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Services;

public class PermissionService
{
    private readonly IUserRepository _userRepository;

    public PermissionService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> UserHasPermissionAsync(string username, string permissionName)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null) return false;
        
        // Simplified check - in real implementation would query through relationships
        return true; // Placeholder for demo
    }

    public async Task<List<string>> GetUserPermissionsAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null) return new List<string>();
        
        // Simplified - in real implementation would query through relationships
        return new List<string> { "user.read" }; // Placeholder for demo
    }
}