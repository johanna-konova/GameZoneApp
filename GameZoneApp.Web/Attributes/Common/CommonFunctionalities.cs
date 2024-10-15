using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace GameZoneApp.Web.Attributes.Common
{
    public static class CommonFunctionalities
    {
        public static Guid ParseId(ActionExecutingContext context)
            => context.ActionArguments.TryGetValue("id", out var idObj) && idObj is string idString && Guid.TryParse(idString, out var parsedId)
                ? parsedId
                : context.ActionArguments.TryGetValue("model", out var requestObj)
                    && requestObj != null
                    && requestObj.GetType().GetProperty("Id", typeof(Guid)) is PropertyInfo propertyInfo
                    && propertyInfo.PropertyType == typeof(Guid)
                        ? (Guid)propertyInfo.GetValue(requestObj)!
                        : Guid.Empty;

        public static void HandleError(
            ActionExecutingContext context,
            string messageType,
            string message,
            string actionName,
            string controllerName)
        {
            var controller = (Controller)context.Controller;
            controller.TempData[messageType] = message;

            context.Result = new RedirectToActionResult(actionName, controllerName, null);
        }
    }
}
