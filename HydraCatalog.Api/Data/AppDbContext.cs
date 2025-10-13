using HydraCatalog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraCatalog.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}