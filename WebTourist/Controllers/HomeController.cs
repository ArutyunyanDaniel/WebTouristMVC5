using System.Web.Mvc;
using WebTourist.Models;
using System.Linq;

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
        public ActionResult EventMouseClick(string userLocation)
        {       
            return Json(dbContext.FindNearestWay(userLocation));
        }


        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}