using GameZoneApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using static GameZoneApp.Web.Attributes.Common.CommonFunctionalities;
using static GameZoneApp.Core.Constants.MessageTypes;
using static GameZoneApp.Core.Constants.MessageConstants;
using GameZoneApp.Web.Controllers;

namespace GameZoneApp.Web.Attributes
{
    public class CreatorAttribute : ActionFilterAttribute
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
                }

                var userId = context.HttpContext.User.Id();

                if (await gameService!.IsUserGameCreatorAsync(gameId, userId))
                {
                    await next();
                    return;
                }
            }

            HandleError(context, ErrorMessage, MustBeGameCreator, nameof(GameController.All), "Game");
        }
    }
}
