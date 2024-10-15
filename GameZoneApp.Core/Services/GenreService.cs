using GameZoneApp.Core.Contracts;
using GameZoneApp.Core.Models.Game;
using GameZoneApp.Infrastructure.Common;
using GameZoneApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameZoneApp.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository repository;

        public GenreService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<GameGenreOptionModel>> GetAllAsync()
            => await repository
                .AllAsNoTracking<Genre>()
                .Select(g => new GameGenreOptionModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToListAsync();

        public async Task<bool> ExistsByIdAsync(int id)
            => await repository
                .AllAsNoTracking<Genre>()
                .AnyAsync(g => g.Id == id);
    }
}
