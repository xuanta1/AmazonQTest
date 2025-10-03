#nullable disable

using System.ComponentModel.DataAnnotations;

namespace TestAmazonQ.Models.Requests;

public class CreateUserRequest
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}