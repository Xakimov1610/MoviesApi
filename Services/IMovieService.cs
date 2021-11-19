
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie.Entities;

namespace movie.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie> GetAsync(Guid id);
        Task<List<Movie>> GetAllAsync(string title);
        Task<(bool IsSuccess, Exception Exception)> CreateAsync(Movie movie);
        Task<bool> ExistsAsync(Guid id);
        Task<(bool IsSuccess, Exception Exception, Movie Movie)> UpdateAsync(Movie movie);
        Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);

        Task<(bool IsSuccess, Exception Exception)> CreateAsync(Image image);
    }
}