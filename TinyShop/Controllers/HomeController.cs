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
        [HttpGet]
        public ActionResult Сonfiguration () {
            ViewBag.Message = "Сonfiguration page.";

            return View();
        }
        [HttpPost]
        public ActionResult Сonfiguration (Product product) {
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}