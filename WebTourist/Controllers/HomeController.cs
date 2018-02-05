
using System.Web.Mvc;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using System.Threading.Tasks;
using System.Data.Entity;

using WebTourist.Models;
using System;
using System.Globalization;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;

namespace WebTourist.Controllers
{
    public class HomeController : Controller
    {
        DbContextTourist dbContext = new DbContextTourist();

        public async Task<ActionResult> Index()
        {
            return View(await dbContext.Attractions.ToListAsync());
        }

        [HttpPost]
        public ActionResult EventMouseClick(string userLocation)
        {
            Route route = new Route();
            return Json(route.FindNearestWay(userLocation, dbContext.Routes.ToList()));
        }


        [HttpPost]
        public JsonResult AjaxTest1()
        {
            Route route = new Route();
            return Json(route.GetExcursionRoutes(dbContext.Routes.ToList()));
        }


        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}