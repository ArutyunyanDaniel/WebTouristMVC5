using System.Web.Mvc;
using WebTourist.Models;
using System.Linq;

namespace WebTourist.Controllers
{
    public class HomeController : Controller
    {
        DbContextTourist dbContext = new DbContextTourist();
        public string Save;

        public  ActionResult Index()
        {
            return View(dbContext.GetAttractionAndExcursionRoutes());
        }

        [HttpPost]
        public JsonResult EventMouseClick(string userLocation)
        {
            return Json(dbContext.FindNearestWay(userLocation));
        }

        [HttpPost]
        public JsonResult EventButClickNextRoute(string userLocation)
        {
            //var l = dbContext.SaveTable.ToList();
            //string srr=string.Empty;
            //foreach (var item in l)
            //{
            //    srr += item.IddVisitedExcursionRoute + " ";
            //}
            return Json(dbContext.GetNextRoute(userLocation));
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}