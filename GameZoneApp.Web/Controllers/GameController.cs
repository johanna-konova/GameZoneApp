using GameZoneApp.Core.Contracts;
using GameZoneApp.Core.Models.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using static GameZoneApp.Infrastructure.Data.DataConstants.Game;
using static GameZoneApp.Core.Constants.ModelsMessagesConstants;
using System.Security.Claims;

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

            return RedirectToAction(nameof(All));
        }
    }
}
