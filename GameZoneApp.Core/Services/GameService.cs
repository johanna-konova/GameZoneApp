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
        private readonly IGenreService genreService;

        public GameService(
            IRepository _repository,
            IGenreService _genreService)
        {
            repository = _repository;
            genreService = _genreService;
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

        public async Task<IEnumerable<GameViewModel>> GetByUserIdAsync(Guid userId)
            => await repository
                .AllAsNoTracking<GamersGames>()
                .Where(gg => gg.Game.IsDeleted == false && gg.GamerId == userId)
                .Select(gg => new GameViewModel()
                {
                    Id = gg.Game.Id,
                    Title = gg.Game.Title,
                    ImageUrl = gg.Game.ImageUrl,
                    Genre = gg.Game.Genre.Name,
                    ReleasedOn = gg.Game.ReleasedOn.ToString(ReleasedOnDateTimeFormat),
                    Publisher = gg.Game.Publisher.Email!,
                })
                .ToListAsync();

        public async Task<GameDetailsViewModel?> GetDetailsAsync(Guid id)
            => await repository
                .AllAsNoTracking<Game>()
                .Where(g => g.Id == id && g.IsDeleted == false)
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    ReleasedOn = g.ReleasedOn.ToString(ReleasedOnDateTimeFormat),
                    Genre = g.Genre.Name,
                    Publisher = g.Publisher.Email!,
                })
                .FirstOrDefaultAsync();

        public async Task<GameFormModel?> AssembleGameFormModelAsync(Guid id)
        {
            var game = await repository
                .AllAsNoTracking<Game>()
                .Where(g => g.Id == id && g.IsDeleted == false)
                .Select(g => new GameFormModel()
                {
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    ReleasedOn= g.ReleasedOn.ToString(ReleasedOnDateTimeFormat),
                    GenreId = g.GenreId,
                })
                .FirstOrDefaultAsync();

            if (game != null)
            {
                game.Genres = await genreService.GetAllAsync();
            }

            return game;
        }

        public async Task<GameDeleteViewModel?> GetDetailsForDeleteFormAsync(Guid id)
            => await repository
                .AllAsNoTracking<Game>()
                .Where(g => g.Id == id && g.IsDeleted == false)
                .Select(g => new GameDeleteViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Publisher = g.Publisher.Email!,
                })
                .FirstOrDefaultAsync();

        public async Task<bool> ExistsById(Guid id)
            => await repository
                .AllAsNoTracking<Game>()
                .AnyAsync(g => g.IsDeleted == false && g.Id == id);

        public async Task<bool> IsUserGameCreatorAsync(Guid gameId, Guid userId)
            => await repository
                .AllAsNoTracking<Game>()
                .AnyAsync(g => g.IsDeleted == false && g.Id == gameId && g.PublisherId == userId);

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

        public async Task EditAsync(Guid id, GameFormModel model)
        {
            var gameToEdit = await repository.GetByIdAsync<Game>(id);

            gameToEdit!.Title = model.Title;
            gameToEdit.ImageUrl = model.ImageUrl;
            gameToEdit.Description = model.Description;
            gameToEdit.ReleasedOn = DateTime.ParseExact(
                    model.ReleasedOn,
                    ReleasedOnDateTimeFormat,
                    CultureInfo.InvariantCulture);
            gameToEdit.GenreId = model.GenreId;

            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var gameToDelete = await repository.GetByIdAsync<Game>(id);

            gameToDelete!.IsDeleted = true;

            await repository.SaveChangesAsync();
        }

        public async Task AddToZoneAsync(Guid gameId, Guid userId)
        {
            var gamerGame = await repository
                .AllAsNoTracking<GamersGames>()
                .Where(gg => gg.GameId == gameId && gg.GamerId == userId)
                .FirstOrDefaultAsync();

            if (gamerGame == null)
            {
                gamerGame = new GamersGames()
                {
                    GameId = gameId,
                    GamerId = userId,
                };

                await repository.AddAsync<GamersGames>(gamerGame);
                await repository.SaveChangesAsync();
            }
        }

        public async Task StrikeOutAsync(Guid gameId, Guid userId)
        {
            var gamerGame = repository
                .AllAsNoTracking<GamersGames>()
                .Where(gg => gg.GameId == gameId && gg.GamerId == userId)
                .FirstOrDefault();

            if (gamerGame != null)
            {
                repository.Remove(gamerGame);
                await repository.SaveChangesAsync();
            }
        }
    }
}
