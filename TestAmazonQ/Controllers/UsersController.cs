#nullable disable
#pragma warning disable CS8618
#pragma warning disable CA1031

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAmazonQ.Attributes;
using TestAmazonQ.Constants;
using TestAmazonQ.Models.Requests;
using TestAmazonQ.Services;

namespace TestAmazonQ.Controllers;

/// <summary>
/// Controller for user management operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets paginated list of users
    /// </summary>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Page size (default: 10)</param>
    /// <returns>Paginated list of users</returns>
    [HttpGet]
    [RequirePermission(Permissions.UserRead)]
    public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 10)
    {
        var result = await _userService.GetUsersPagedAsync(pageNumber, pageSize);
        if (result.Success)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Gets user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User details</returns>
    [HttpGet("{id}")]
    [RequirePermission(Permissions.UserRead)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">User creation details</param>
    /// <returns>Created user details</returns>
    [HttpPost]
    [RequirePermission(Permissions.UserCreate)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request.Username, request.Password);
        if (result.Success)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="id">User ID to update</param>
    /// <param name="request">User update details</param>
    /// <returns>Updated user details</returns>
    [HttpPut("{id}")]
    [RequirePermission(Permissions.UserUpdate)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
    {
        var result = await _userService.UpdateUserAsync(id, request.Username, request.Password);
        if (result.Success)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id">User ID to delete</param>
    /// <returns>Success response</returns>
    [HttpDelete("{id}")]
    [RequirePermission(Permissions.UserDelete)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }
}