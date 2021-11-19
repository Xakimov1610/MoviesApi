using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using movie.Mappers;
using movie.Models;
using movie.Services;

namespace movie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _ms;
        private readonly IGenreService _gs;
        private readonly IActorService _as;

        public MovieController(
            IMovieService movieService,
            IGenreService genreService,
            IActorService actorService)
        {
            _ms = movieService;
            _gs = genreService;
            _as = actorService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(NewMovie movie)
        {
            if(movie.ActorIds.Count() < 1 || movie.GenreIds.Count() < 1)
            {
                return BadRequest("Actors and Genres are required");
            }
          
            if(!movie.GenreIds.All(id => _as.ExistsAsync(id).Result))
            {
                 return BadRequest("Genre doesnt exist");
            }

            if(!movie.ActorIds.All(id => _as.ExistsAsync(id).Result))
            {
                 return BadRequest("Actor doesnt exist");
            }
          
           var genres = movie.GenreIds.Select(id => _gs.GetAsync(id).Result);
           var actors = movie.ActorIds.Select(id => _as.GetAsync(id).Result);

           var result = await _ms.CreateAsync(movie.ToEntity(actors,genres));

            if(result.IsSuccess)
           {
                return Ok();
           }

           return BadRequest(result.Exception.Message);

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(await _ms.GetAllAsync(), options);
        return Ok(json);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetActionAsync(Guid id)
        {
         if(await _ms.ExistsAsync(id))
        {
            return Ok(await _ms.GetAsync(id));
        }

        return NotFound();
        }

       [HttpPut]
       [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]NewMovie movie)
      {

        var genres = movie.GenreIds.Select(id => _gs.GetAsync(id).Result);
        var actors = movie.ActorIds.Select(id => _as.GetAsync(id).Result);

        var toEntity = movie.ToEntity(actors,genres);
        var insertResult = await _ms.CreateAsync(toEntity);
        
        if(insertResult.IsSuccess)
        {
           return Ok();
        }

        return BadRequest(insertResult.Exception.Message);
      }

    [HttpDelete]
    [Route("{Id}")]
    public async Task<IActionResult> Delete([FromRoute]Guid Id)
     =>Ok(await _ms.DeleteAsync(Id));
  }
}