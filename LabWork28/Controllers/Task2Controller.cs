using System.Collections;
using LabWork28.Extensions;
using LabWork28.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabWork28.Controllers;

[Route("[controller]")]
public class Task2Controller : Controller
{
    private readonly ProductsDbContext _productsDbContext;

    public Task2Controller(ProductsDbContext productsDbContext)
    {
        _productsDbContext = productsDbContext;
    }
    
    [HttpGet]
    [Route("Get")]
    public async Task<ICollection<Product>> Get()
    {
        return await _productsDbContext.Products.ToListAsync();
    }

    [HttpGet]
    [Route("OrderByColumn")]
    public async Task<ICollection<Product>> GetTask3(string column)
    {
        var columns = new[]
        {
            nameof(Product.Id),
            nameof(Product.Name),
            nameof(Product.Price)
        };

        if (!columns.Contains(column))
            throw new ArgumentException(nameof(column));
        
        return await _productsDbContext.Products.OrderBy(column).ToArrayAsync();
    }

    [HttpGet]
    [Route("Task4")]
    public async Task<ICollection<Product>> GetTask4(string column1, string? column2)
    {
        var columns = new[]
        {
            nameof(Product.Id),
            nameof(Product.Name),
            nameof(Product.Price)
        };

        if (!columns.Contains(column1))
            throw new ArgumentException(nameof(column1) + nameof(column2));
        
        var query = _productsDbContext.Products
            .OrderBy(column1);

        if (column2 is null) 
            return await query.ToListAsync();
        
        if (!columns.Contains(column2))
            throw new ArgumentException(nameof(column2));

        return await query
            .ThenBy(column2)
            .ToListAsync();
    }
}