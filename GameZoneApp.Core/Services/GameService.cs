using GameZoneApp.Core.Contracts;
using GameZoneApp.Core.Models.Game;
using GameZoneApp.Infrastructure.Common;
using GameZoneApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using static GameZoneApp.Infrastructure.Data.DataConstants.Game;

namespace GameZoneApp.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository repository;

        public GameService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<GameViewModel>> GetAllAsync()
            => await repository
                .AllAsNoTracking<Game>()
                .Where(g => g.IsDeleted == false)
                .Select(g => new GameViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Genre = g.Genre.Name,
                    ReleasedOn = g.ReleasedOn.ToString(ReleasedOnDateTimeFormat),
                    Publisher = g.Publisher.Email!,
                })
                .ToListAsync();

        public async Task AddAsync(GameFormModel model, Guid userId)
        {
            Game game = new Game()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                ReleasedOn = DateTime.ParseExact(
                    model.ReleasedOn,
                    ReleasedOnDateTimeFormat,
                    CultureInfo.InvariantCulture),
                GenreId = model.GenreId,
                PublisherId = userId,
            };

            await repository.AddAsync(game);
            await repository.SaveChangesAsync();
        }
    }
}
