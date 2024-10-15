using GameZoneApp.Core.Contracts;
using GameZoneApp.Core.Models.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameZoneApp.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGenreService genreService;

        public GameController(IGenreService _genreService)
        {
            genreService = _genreService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            var model = new GameFormModel()
            {
                Genres = await genreService.GetAllAsync()
            };

            return View(model);
        }
    }
}
