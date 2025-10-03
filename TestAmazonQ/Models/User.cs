#nullable disable

using System.ComponentModel.DataAnnotations;

namespace TestAmazonQ.Models;

/// <summary>
/// User entity model
/// </summary>
public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9_]+$")]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}