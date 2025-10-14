using HydraWarehouse.Api.Data;
using HydraWarehouse.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HydraWarehouse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly WarehouseDbContext _context;

    public StockController(WarehouseDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStockItems()
    {
        var items = await _context.StockItems.ToListAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddStockItem([FromBody]StockItem stockItem)
    {
        stockItem.Id = Guid.NewGuid();
        _context.StockItems.Add(stockItem);
        
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetStockItemById", new { id = stockItem.Id }, stockItem);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockItemById(Guid id)
    {
        var stockItem = await _context.StockItems.FindAsync(id);
        if (stockItem == null)
        {
            return NotFound();
        }
        return Ok(stockItem);
    }

}