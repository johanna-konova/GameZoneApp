using GameZoneApp.Core.Contracts;
using GameZoneApp.Core.Models.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using static GameZoneApp.Infrastructure.Data.DataConstants.Game;
using static GameZoneApp.Core.Constants.MessageConstants;
using static GameZoneApp.Core.Constants.MessageTypes;
using static GameZoneApp.Core.Constants.ModelsMessagesConstants;
using System.Security.Claims;
using GameZoneApp.Web.Attributes;
using Microsoft.CodeAnalysis.Differencing;

namespace GameZoneApp.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGenreService genreService;
        private readonly IGameService gameService;

        public GameController(
            IGenreService _genreService,
            IGameService _gameService)
        {
            genreService = _genreService;
            gameService = _gameService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> All()
        {
            var games = await gameService.GetAllAsync();

            return View(games);
        }

        public async Task<IActionResult> MyZone()
        {
            var gamesByGamerId = await gameService.GetByUserIdAsync(User.Id());

            return View(gamesByGamerId);
        }

        [ExistingGame]
        public async Task<IActionResult> Details(string id)
        {
            var game = await gameService.GetDetailsAsync(Guid.Parse(id));

            return View(game);
        }

        public async Task<IActionResult> Add()
        {
            var model = new GameFormModel()
            {
                Genres = await genreService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameFormModel model)
        {
            if (DateTime.TryParseExact(
                model.ReleasedOn,
                ReleasedOnDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), string.Format(InvalidDateTimeFormat, ReleasedOnDateTimeFormat));
            }

            if (await genreService.ExistsByIdAsync(model.GenreId) == false)
            {
                ModelState.AddModelError(nameof(model.GenreId), NonExistentGenre);
            }

            if (!ModelState.IsValid)
            {
                model.Genres = await genreService.GetAllAsync();
                return View(model);
            };

            await gameService.AddAsync(model, User.Id());

            TempData[SuccessMessage] = SuccessfullyAddedGame;

            return RedirectToAction(nameof(All));
        }

        [ExistingGame]
        [Creator]
        public async Task<IActionResult> Edit(string id)
        {
            var gameToEdit = await gameService.AssembleGameFormModelAsync(Guid.Parse(id));

            return View(gameToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameFormModel model, string id)
        {
            if (DateTime.TryParseExact(
                model.ReleasedOn,
                ReleasedOnDateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _) == false)
            {
                ModelState.AddModelError(nameof(model.ReleasedOn), string.Format(InvalidDateTimeFormat, ReleasedOnDateTimeFormat));
            }

            if (await genreService.ExistsByIdAsync(model.GenreId) == false)
            {
                ModelState.AddModelError(nameof(model.GenreId), NonExistentGenre);
            }

            if (!ModelState.IsValid)
            {
                model.Genres = await genreService.GetAllAsync();
                return View(model);
            };

            await gameService.EditAsync(Guid.Parse(id), model);

            TempData[SuccessMessage] = SuccessfullyEditedGame;

            return RedirectToAction(nameof(All));
        }

        [ExistingGame]
        [Creator]
        public async Task<IActionResult> Delete(string id)
        {
            var gameToDelete = await gameService.GetDetailsForDeleteFormAsync(Guid.Parse(id));

            return View(gameToDelete);
        }

        [ExistingGame]
        [Creator]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(GameDeleteViewModel model)
        {
            await gameService.DeleteAsync(model.Id);

            TempData[SuccessMessage] = SuccessfullyDeletedGame;

            return RedirectToAction(nameof(MyZone));
        }

        [ExistingGame]
        public async Task<IActionResult> AddToMyZone(string id)
        {
            await gameService.AddToZoneAsync(Guid.Parse(id), User.Id());

            TempData[SuccessMessage] = SuccessfullyAddedGameToMyZone;

            return RedirectToAction(nameof(MyZone));
        }

        [ExistingGame]
        public async Task<IActionResult> StrikeOut(string id)
        {
            await gameService.StrikeOutAsync(Guid.Parse(id), User.Id());

            TempData[SuccessMessage] = SuccessfullyStrikeOutGame;

            return RedirectToAction(nameof(MyZone));
        }
    }
}
