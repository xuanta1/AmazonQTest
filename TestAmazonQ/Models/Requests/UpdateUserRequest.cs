#nullable disable

using System.ComponentModel.DataAnnotations;

namespace TestAmazonQ.Models.Requests;

public class UpdateUserRequest
{
    [Required]
    public string Username { get; set; }
    
    public string Password { get; set; }
}