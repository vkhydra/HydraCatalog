using HydraWarehouse.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraWarehouse.Api.Data;

public class WarehouseDbContext : DbContext
{  
    public WarehouseDbContext (DbContextOptions<WarehouseDbContext> options) : base(options){}
    
    public DbSet<StockItem> StockItems { get; set; }
}