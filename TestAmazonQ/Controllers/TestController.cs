#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestAmazonQ.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult GetPublic()
    {
        return Ok(new { message = "This is a public endpoint" });
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult GetProtected()
    {
        var username = User.Identity?.Name;
        return Ok(new { message = $"Hello {username}, this is a protected endpoint" });
    }
}