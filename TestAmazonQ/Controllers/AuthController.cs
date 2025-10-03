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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _userService.ValidateUserAsync(request.Username, request.Password);
        if (result.Success)
        {
            var token = _jwtService.GenerateToken(result.Data.Username);
            var response = new LoginResponse
            {
                Token = token,
                Expires = DateTime.Now.AddHours(1)
            };
            return Ok(ApiResponse<LoginResponse>.Ok(response, result.Message));
        }

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        var result = await _userService.CreateUserAsync(request.Username, request.Password);
        if (result.Success)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // JWT là stateless, logout chỉ cần client xóa token
        return Ok(new { message = Messages.LoggedOutSuccessfully });
    }
}