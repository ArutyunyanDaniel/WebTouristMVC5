using System.Web.Mvc;
using WebTourist.Models;

namespace WebTourist.Controllers
{
    public class HomeController : Controller
    {
        DbContextTourist dbContext = new DbContextTourist();

        public  ActionResult Index()
        {
            return View(dbContext.GetAttractionAndExcursionRoutes());
        }

        [HttpPost]
        public JsonResult EventMouseClick(RouteInformation routeInformation)
        {   
            return Json(dbContext.FindNearestWay(routeInformation));
        }

        [HttpPost]
        public JsonResult EventButClickNextRoute(RouteInformation routeInformation)
        {
            return Json(dbContext.GetNextRoute(routeInformation));
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}