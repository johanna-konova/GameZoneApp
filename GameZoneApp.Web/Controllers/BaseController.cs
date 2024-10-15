using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZoneApp.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
