using HydraCatalog.Api.Data;
using HydraCatalog.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HydraCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        // Simples validação para garantir que a categoria existe
        var category = await _context.Categories.FindAsync(product.CategoryId);
        if (category == null)
        {
            // Para simplificar, vamos criar a categoria se ela não existir.
            // Em um projeto real, você poderia retornar um erro.
            var newCategory = new Category { Name = "Default Category" };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            product.CategoryId = newCategory.Id;
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
}