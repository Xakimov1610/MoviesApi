using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using movie.Mappers;
using movie.Models;
using movie.Services;

namespace movie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _as;

    public ActorController(IActorService actorservice)
    {
      _as = actorservice;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(NewActor actor)
    {
        var result = await _as.CreateAsync(actor.ToEntity());

        if(result.IsSuccess)
        {
           return Ok();
        }

        return BadRequest(result.Exception.Message);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetActionAsync(Guid id)
    {
        if(await _as.ExistsAsync(id))
        {
            return Ok(await _as.GetAsync(id));
        }

        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    =>Ok(await _as.GetAllAsync());

   
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]NewActor actor)
    {
        var toEntity = actor.ToEntity();
        var insertResult = await _as.CreateAsync(toEntity);
        
        if(insertResult.IsSuccess)
        {
           return Ok();
        }

        return BadRequest(insertResult.Exception.Message);
    }
   
   [HttpDelete]
   [Route("{Id}")]
    public async Task<IActionResult> Delete([FromRoute]Guid Id)
    =>Ok(await _as.DeleteAsync(Id));

 }
}