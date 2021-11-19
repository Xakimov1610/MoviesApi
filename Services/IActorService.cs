using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using movie.Entities;

namespace movie.Services
{
    public interface IActorService
    {
        Task<List<Actor>> GetAllAsync();
        Task<Actor> GetAsync(Guid id);
        Task<List<Actor>> GetAllAsync(string fullname);
        Task<(bool IsSuccess, Exception Exception)> CreateAsync(Actor actor);
        Task<bool> ExistsAsync(Guid id);
        Task<(bool IsSuccess, Exception Exception, Actor Actor)> UpdateAsync(Actor actor);
        Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Guid id);
    }
}