    using System.Web.Mvc;
using WebTourist.Models;
using System.Collections.Generic;
using System.Linq;

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
        public JsonResult EventCheckBoxClick(string city)
        {
            return Json(dbContext.GetExcursionRoutes(city), JsonRequestBehavior.AllowGet);
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