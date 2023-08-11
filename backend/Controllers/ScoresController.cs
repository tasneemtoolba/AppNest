using DealsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealsApp.Controllers;

[Route("[controller]")]
[ApiController]
public class ScoresController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ScoresController(ApiDbContext context)
    {
        _context = context;
    }

    // GET: Scores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Score>>> GetScores()
    {
        return await _context.Scores.ToListAsync();
    }


    [HttpGet("{searchQuery}")]
        public  List<Score> GetScores( String searchQuery,  [FromQuery] int? limit){
        var filteredResults = _context.Scores.Where(score => score.product_name.Contains(searchQuery)).OrderByDescending(e=>e.score_value);
        if(limit != null){
            return filteredResults.Take((int)limit).ToList();
            }
        return filteredResults.ToList();
    }

    // POST: Scores
    [HttpPost]
    public async Task<ActionResult<Score>> PostScore(Score scoreInstance)
    {
        _context.Scores.Add(scoreInstance);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetScore", new { id = scoreInstance.id }, scoreInstance);
    }

    // PUT: Scores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScore(String id, Score scoreInstance)
    {
        if (id != scoreInstance.id)
        {
            return BadRequest();
        }

        _context.Entry(scoreInstance).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ScoreExists(id))
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

    // DELETE: Scores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScore(String id)
    {
        var scoreInstance = await _context.Scores.FindAsync(id);
        if (scoreInstance == null)
        {
            return NotFound();
        }

        _context.Scores.Remove(scoreInstance);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool ScoreExists(String id)
    {
        return _context.Scores.Any(e => e.id == id);
    }

}
