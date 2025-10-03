#nullable disable
using System.ComponentModel.DataAnnotations;

namespace TestAmazonQ.Models;

public class RolePermission
{
    [Key]
    public int Id { get; set; }
    
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    
    public DateTime AssignedAt { get; set; } = DateTime.Now;
    
    // Navigation properties
    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
}