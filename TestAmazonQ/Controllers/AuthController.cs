#nullable disable
#pragma warning disable CS8618
#pragma warning disable CA1031

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAmazonQ.Constants;
using TestAmazonQ.Models.Requests;
using TestAmazonQ.Models.Responses;
using TestAmazonQ.Services;

namespace TestAmazonQ.Controllers;

/// <summary>
/// Controller for authentication operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly UserService _userService;

    public AuthController(JwtService jwtService, UserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }

    /// <summary>
    /// Authenticates user and returns JWT token
    /// </summary>
    /// <param name="request">Login credentials</param>
    /// <returns>JWT token if authentication successful</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.BadRequest("Invalid input data"));
        }

        try
        {
            var result = await _userService.ValidateUserAsync(request.Username, request.Password);
            if (result.Success)
            {
                var token = _jwtService.GenerateToken(result.Data.Username);
                var response = new LoginResponse
                {
                    Token = token,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };
                return Ok(ApiResponse<LoginResponse>.Ok(response, result.Message));
            }

            return StatusCode(result.StatusCode, result);
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object>.InternalServerError("Authentication service temporarily unavailable"));
        }
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="request">Registration details</param>
    /// <returns>Success response if registration successful</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.BadRequest("Invalid input data"));
        }

        try
        {
            var result = await _userService.CreateUserAsync(request.Username, request.Password);
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(result.StatusCode, result);
        }
        catch (Exception)
        {
            return StatusCode(500, ApiResponse<object>.InternalServerError("Registration service temporarily unavailable"));
        }
    }

    /// <summary>
    /// Logs out the current user
    /// </summary>
    /// <returns>Success message</returns>
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // JWT là stateless, logout chỉ cần client xóa token
        return Ok(new { message = Messages.LoggedOutSuccessfully });
    }
}