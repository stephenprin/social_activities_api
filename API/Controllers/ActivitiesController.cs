using Application.Activities.Command;
using Application.Activities.DTOs;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController() : BaseApiController
{

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await Mediator.Send(new GetActivityList.Query());
    }
    [Authorize]

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(string id)
    {
        var result = await Mediator.Send(new GetActivityDetail.Query { Id = id });
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(CreateActivityDto createActivityDto)
    {
        return await Mediator.Send(new CreateActivity.Command { createActivityDto = createActivityDto });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity( Activity activity)
    {
        await Mediator.Send(new EditActivity.Command { Activity = activity });
        return NoContent();
        
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        await Mediator.Send(new DeleteActivity.Command { Id = id });
        return NoContent();
    }
}
