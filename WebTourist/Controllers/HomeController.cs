using System.Web.Mvc;
using WebTourist.Models;

namespace WebTourist.Controllers
{
    public class HomeController : Controller
    {
        DbContextTourist dbContext = new DbContextTourist();

        public  ActionResult Index()
        {
            return View(dbContext.GetAttractions());
        }

        [HttpPost]
        public JsonResult EventCitySelect(string city)
        {
            int idSelectedCity = dbContext.GetIdCurrentCity(city);
            bool succes = true;
            if(idSelectedCity == -1)
                succes = false;
            
            return Json(new { success = succes, idSelectedCity });
        }

        [HttpPost]
        public JsonResult EventCheckBoxClick(int idCurrentCity)
        {
            return Json(dbContext.GetExcursionRoutes(idCurrentCity), JsonRequestBehavior.AllowGet);
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