#nullable disable

namespace TestAmazonQ.Models.Responses;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}