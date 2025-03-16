using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController(AppDbContext context) : BaseApiController
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _context.Activities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == id.ToString());
        if (activity == null)
        {
            return NotFound();
        }
        return activity;
    }
}
