using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DealsApp.Models;
using Newtonsoft.Json;

namespace DealsApp.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ProductsController(ApiDbContext context)
    {
        _context = context;
    }

    // GET: Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {

        var products = await _context.Products.ToListAsync();
        var serializedProducts = JsonConvert.SerializeObject(products);
        System.Diagnostics.Debug.WriteLine(serializedProducts);
        System.Diagnostics.Debug.WriteLine(products);
        return Content(serializedProducts, "application/json");

        // return await _context.Products.ToListAsync();
    }

    // GET: Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(String id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: Products
    [HttpPost]
    // public async Task<ActionResult<Product>> PostProduct(Product product)
    // {
    public async Task<ActionResult<Product>> Create(Product product)
    {


        // var product = JsonConvert.DeserializeObject<Product>(requestBody.GetRawText());
        _context.Products.Add(product);
        // System.Diagnostics.Debug.WriteLine(requestBody.GetString());
        await _context.SaveChangesAsync();

        // System.Diagnostics.Debug.WriteLine(requestBody.GetRawText());
        // System.Diagnostics.Debug.WriteLine(product);
        if(product.deals!=null){
            for(int i = 0;i < product.deals.Count;++i){
                        _context.Deals.Add(product.deals[i]);

            }
                    await _context.SaveChangesAsync();

            }
        // return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

        return CreatedAtAction("GetProduct", new { id = product.id }, product);
    }

    // PUT: Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(String id, Product product)
    {
        if (id != product.id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(String id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(String id)
    {
        return _context.Products.Any(e => e.id == id);
    }
}

