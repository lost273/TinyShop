using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyShop.Filters;
using TinyShop.Models;

namespace TinyShop.Controllers {
    [MyAuth]
    public class HomeController : Controller {
        OneDayContext db = new OneDayContext();
        public ActionResult Index () {
            return View(db.Products);
        }

        public ActionResult Diagram () {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Сonfiguration () {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}