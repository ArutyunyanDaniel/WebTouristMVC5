using System.Web.Mvc;
using WebTourist.Models;
using System.Linq;
using GMap.NET;
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
        public JsonResult EventMouseClick(Point point)
        {   
            return Json(dbContext.FindNearestWay(point));
        }


        [HttpPost]
        public JsonResult EventButClickNextRoute(Point point)
        {
            return Json(dbContext.GetNextRoute(point));
        }



        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}