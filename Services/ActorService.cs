using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using movie.Data;
using movie.Entities;

namespace movie.Services
{
    public class ActorService : IActorService
    {
        private readonly MoviesDbContext _ctx;
        private readonly ILogger<ActorService> _logger;

        public ActorService(MoviesDbContext context, ILogger<ActorService> logger)
        {
            _ctx = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception)> CreateAsync(Actor actor)
        {
        try
        {
            await _ctx.Actors.AddAsync(actor);
            await _ctx.SaveChangesAsync();

            return (true, null);
        }
        catch(Exception e)
        {
            return (false, e);
        }
            
        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id)
        {

        try
        {
        var actor = await GetAsync(id);
        if(actor == default(Actor))
        {
            return (false, new ArgumentException("Not found."));
        }
            _ctx.Actors.Remove(actor);
            await _ctx.SaveChangesAsync();

            return (true, null);
        }
        catch(Exception e)
        {
            return (false, e);
        }
        }

        public Task<bool> ExistsAsync(Guid id)
        =>_ctx.Actors.AnyAsync(g => g.Id == id);

        public Task<List<Actor>> GetAllAsync()
        =>_ctx.Actors.ToListAsync();

         public Task<List<Actor>> GetAllAsync(string fullname)
        =>_ctx.Actors
        .AsNoTracking()
        .Where(a => a.Fullname == fullname)
        .ToListAsync();

        public Task<Actor> GetAsync(Guid id)
        
         => _ctx.Actors.FirstOrDefaultAsync(g => g.Id == id);
        

        public async Task<(bool IsSuccess, Exception Exception, Actor Actor)> UpdateAsync(Actor actor)
        {
        if(!await ExistsAsync(actor.Id))
        {
            return (false, new ArgumentException($"There is no Actor with given ID: {actor.Id}"), null);
        }

        await _ctx.Actors.AnyAsync(t => t.Id == actor.Id);

        _ctx.Actors.Update(actor);
        await _ctx.SaveChangesAsync();

        return (true, null, actor);
        }
    }
}