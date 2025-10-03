#nullable disable

using Microsoft.EntityFrameworkCore;
using TestAmazonQ.Models;

namespace TestAmazonQ.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}