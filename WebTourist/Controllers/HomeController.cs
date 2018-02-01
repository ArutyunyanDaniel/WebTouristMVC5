using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Threading.Tasks;
using System.Data.Entity;

using WebTourist.Models;
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