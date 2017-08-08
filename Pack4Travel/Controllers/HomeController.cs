using Pack4Travel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pack4Travel.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var ThreeTheBestEquipementList = db.equipements.OrderByDescending(e => e.five_stars).Take(3).ToList();
            
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "equipements");
            }
            return View(ThreeTheBestEquipementList);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}