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
    public class MovieService : IMovieService
    {
        private readonly MoviesDbContext _ctx;
        private readonly ILogger<MovieService> _logger;

        public MovieService(MoviesDbContext context, ILogger<MovieService> logger)
        {
            _ctx = context;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception)> CreateAsync(Movie movie)
        {
         try
        {
            await _ctx.Movies.AddAsync(movie);
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
        var movie = await GetAsync(id);
        if(movie == default(Movie))
        {
            return (false, new ArgumentException("Not found."));
        }
            _ctx.Movies.Remove(movie);
            await _ctx.SaveChangesAsync();

            return (true, null);
        }
        catch(Exception e)
        {
            return (false, e);
        }
        }

        public Task<bool> ExistsAsync(Guid id)
         =>_ctx.Movies.AnyAsync(g => g.Id == id);
        public Task<List<Movie>> GetAllAsync()
          =>_ctx.Movies
          .AsNoTracking()
          .Include(m => m.Actors)
          .Include(m => m.Genres)
          .ToListAsync();

         public Task<List<Movie>> GetAllAsync(string title)
          =>_ctx.Movies
          .AsNoTracking()
          .Where(a => a.Title == title) 
          .Include(m => m.Actors)
          .Include(m => m.Genres)
          .ToListAsync();

        public Task<Movie> GetAsync(Guid id)
        => _ctx.Movies.FirstOrDefaultAsync(g => g.Id == id);

        public async Task<(bool IsSuccess, Exception Exception, Movie Movie)> UpdateAsync(Movie movie)
        {
         if(!await ExistsAsync(movie.Id))
        {
            return (false, new ArgumentException($"There is no MOvie with given ID: {movie.Id}"), null);
        }

        _ctx.Movies.Update(movie);
        await _ctx.SaveChangesAsync();

        return (true, null, movie);
        }
    }
}