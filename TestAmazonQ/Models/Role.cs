#nullable disable
using System.ComponentModel.DataAnnotations;

namespace TestAmazonQ.Models;

/// <summary>
/// Role entity model
/// </summary>
public class Role
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [StringLength(200)]
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Navigation properties
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}