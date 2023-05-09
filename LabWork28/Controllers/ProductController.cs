using LabWork28.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabWork28.Controllers;

public class ProductController : BaseController
{
    private readonly ProductsDbContext _context;

    public ProductController(ProductsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts() =>
        await _context.Products
            .AsNoTrackingWithIdentityResolution()
            .ToArrayAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (product is null)
            return BadRequest();

        return product;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutProduct([FromBody] Product product)
    {
        _context.Products.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (product is null)
            return BadRequest();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok();
    }
}