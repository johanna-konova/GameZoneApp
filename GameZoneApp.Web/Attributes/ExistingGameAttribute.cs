using GameZoneApp.Core.Contracts;
using GameZoneApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using static GameZoneApp.Core.Constants.MessageConstants;
using static GameZoneApp.Core.Constants.MessageTypes;
using static GameZoneApp.Web.Attributes.Common.CommonFunctionalities;

namespace HouseRentingSystem.Web.Attributes
{
    public class ExistingGameAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Guid gameId = ParseId(context);

            if (gameId != Guid.Empty)
            {
                IGameService? gameService =
                    context.HttpContext.RequestServices.GetService<IGameService>();

                if (gameService == null)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    return;
                }

                if (await gameService.ExistsById(gameId))
                {
                    await next();
                    return;
                }
            }

            HandleError(context, ErrorMessage, NonExistentPage, nameof(GameController.All), "Game");
        }
    }
}
