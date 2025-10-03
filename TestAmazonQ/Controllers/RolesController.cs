#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAmazonQ.Attributes;
using TestAmazonQ.Constants;
using TestAmazonQ.Services;

namespace TestAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly RoleService _roleService;

    public RolesController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("assign")]
    [RequirePermission(Permissions.RoleUpdate)]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        var result = await _roleService.AssignRoleToUserAsync(request.UserId, request.RoleId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("remove")]
    [RequirePermission(Permissions.RoleUpdate)]
    public async Task<IActionResult> RemoveRole([FromBody] AssignRoleRequest request)
    {
        var result = await _roleService.RemoveRoleFromUserAsync(request.UserId, request.RoleId);
        return StatusCode(result.StatusCode, result);
    }
}

public class AssignRoleRequest
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}