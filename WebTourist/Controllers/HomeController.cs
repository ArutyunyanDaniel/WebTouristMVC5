using System.Web.Mvc;
using WebTourist.Models;

namespace WebTourist.Controllers
{
    public class HomeController : Controller
    {
        DbContextTourists dbContext = new DbContextTourists();

        public  ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EventCitySelect(string city)
        {
            return Json(dbContext.GetIdCurrentCity(city));
        }

        [HttpPost]
        public JsonResult EventGetAttractions(int idCurrentCity)
        {
            return Json(dbContext.GetAttractions(idCurrentCity));
        }

        [HttpPost]
        public JsonResult EventCheckBoxClick(int idCurrentCity)
        {
            return Json(dbContext.GetExcursionRoutes(idCurrentCity));
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