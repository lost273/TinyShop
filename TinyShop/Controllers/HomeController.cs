using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyShop.Filters;
using TinyShop.Models;

namespace TinyShop.Controllers {
    [MyAuth]
    public class HomeController : Controller {
        OneDayContext db = new OneDayContext();
        public ActionResult Index (int year = 1, int month = 1, int day = 1) {
            ViewBag.Products = db.Products;
            ViewBag.Time = DateTime.Today.Date;
            DateTime dateRequest = new DateTime(year, month, day);
            // if user went from main page
            if (dateRequest == DateTime.MinValue) {
                dateRequest = DateTime.Today;
            }
            var dayConsumptions = db.Сonsumptions.Where(prod => prod.Date == dateRequest).ToList();
           
            if (dayConsumptions == null) {
                return View(new Сonsumption { Name = "", Quantity = 0, Cost = 0 });
            }
            return View(dayConsumptions);
        }

        public ActionResult Diagram () {
            ViewBag.Message = "Diagram page.";
            return View();
        }
        [HttpGet]
        public ActionResult Сonfiguration () {
            return View(db.Products.ToList());
        }
        [HttpPost]
        public ActionResult Сonfiguration (Product product) {
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Сonfiguration");
        }
        [HttpPost]
        public ActionResult ChangeProduct (Product product, string action) {
            if (action == "change") {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (action == "delete") {
                Product p = db.Products.Find(product.ProductId);
                db.Products.Remove(p);
                db.SaveChanges();
            }
            
            return RedirectToAction("Сonfiguration");
        }
    }
}