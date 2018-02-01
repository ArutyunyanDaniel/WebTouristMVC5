
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

namespace WebTourist.Controllers
{
    public class HomeController : Controller
    {
        DbContextTourist dbContext = new DbContextTourist();
        public async Task<ActionResult> Index()
        {
            return View(await dbContext.Attractions.ToListAsync());
        }
    }
}