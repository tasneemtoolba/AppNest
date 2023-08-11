using DealsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealsApp.Controllers;

[Route("[controller]")]
[ApiController]
public class DealsController : ControllerBase
{
    private readonly ApiDbContext _context;

    public DealsController(ApiDbContext context)
    {
        _context = context;
    }

    // GET: Deals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Deal>>> GetDeals()
    {
        return await _context.Deals.ToListAsync();
    }

    // GET: Deals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Deal>> GetDeal(String id)
    {
        var dealInstance = await _context.Deals.FindAsync(id);

        if (dealInstance == null)
        {
            return NotFound();
        }

        return dealInstance;
    }

    // GET: Deals/
    [HttpGet("by-product/{productId}")]
    public async Task<ActionResult<IEnumerable<Deal>>> GetDealsByProductId(String productId)
    {
        var deals = await _context.Deals.Where(deal => deal.Productid == productId).ToListAsync();

        if (deals == null )
        {
            return NotFound();
        }
        else if( deals.Count == 0){
            return Ok(new List<Deal>()); // Return an empty list
        }

        return deals;
    }
    // POST: Deals
    [HttpPost]
    public async Task<ActionResult<Deal>> PostDeal(Deal dealIinstance)
    {
        _context.Deals.Add(dealIinstance);
        await _context.SaveChangesAsync();
        var product = await _context.Products.FindAsync(dealIinstance.Productid);
        if (product == null)
        {
            return NotFound("Product not found");
        }
        var score = new Score
        {
              id = Guid.NewGuid().ToString(),
            Productid = dealIinstance.Productid,
            Dealid = dealIinstance.id,
            product_name = product.name,
            score_value = (product.price- dealIinstance.discount_percentage)*(dealIinstance.discount_percentage/100.0) // You can calculate the score value based on your logic
        };
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetDeal", new { id = dealIinstance.id }, dealIinstance);
    }

    // PUT: Deals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeal(String id, Deal dealInstance)
    {
        if (id != dealInstance.id)
        {
            return BadRequest();
        }

        _context.Entry(dealInstance).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DealExists(id))
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

    // DELETE: Deals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDeal(String id)
    {
        var deal = await _context.Deals.FindAsync(id);
        if (deal == null)
        {
            return NotFound();
        }
         // Find the associated score using the deal's id and delete it
        var score = await _context.Scores.FirstOrDefaultAsync(s => s.Dealid == id);
        if (score != null)
        {
            _context.Scores.Remove(score);
        }


        _context.Deals.Remove(deal);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DealExists(String id)
    {
        return _context.Deals.Any(e => e.id == id);
    }
}
