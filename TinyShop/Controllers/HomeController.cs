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
        [HttpGet]
        public ActionResult Index (int year = 1, int month = 1, int day = 1) {
            ViewBag.Products = db.Products;
            ViewBag.Total = 0;
            DateTime dateRequest = new DateTime(year, month, day);
            // if user went from the main page
            if (dateRequest == DateTime.MinValue) {
                dateRequest = DateTime.Today;
                ViewBag.Time = DateTime.Today.Date;
            }
            else {
                ViewBag.Time = dateRequest.Date;
            }
            var currentDay = db.Rows.Where(row => row.Date == dateRequest).ToList();
            return View(currentDay);
        }
        [HttpPost]
        public ActionResult Index(Row row) {
            row.Total = row.Quantity * row.Cost;
            db.Rows.Add(row);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new { year = row.Date.Year, month = row.Date.Month, day = row.Date.Day });
        }
        public ActionResult Diagram (int yearOne = 1, int monthOne = 1, int yearTwo = 1, int monthTwo = 1) {
            DateTime dateRequestOne = new DateTime(yearOne, monthOne, 01);
            DateTime dateRequestTwo = new DateTime(yearTwo, monthTwo, 01);
            ChartInfo chart = new ChartInfo();
            int namesCount;

            List<Row> rowsOne = db.Rows.Where(row => row.Date.Month == dateRequestOne.Month).ToList();
            List<Row> rowsTwo = db.Rows.Where(row => row.Date.Month == dateRequestTwo.Month).ToList();
            chart.Years = db.Rows.Select(r => r.Date.Year).Distinct().ToList();
            chart.Months = db.Rows.Select(r => r.Date.Month).Distinct().ToList();
            chart.ChartNamesOne = rowsOne.Select(n => n.Name).Distinct().ToList();
            chart.ChartNamesTwo = rowsTwo.Select(n => n.Name).Distinct().ToList();
            chart.ChartTotalOne = FillTheChart(rowsOne, chart.ChartNamesOne);
            chart.ChartTotalTwo = FillTheChart(rowsTwo, chart.ChartNamesTwo);

            if (chart.ChartNamesOne.Count >= chart.ChartNamesTwo.Count) {
                namesCount = chart.ChartNamesOne.Count;
            }
            else {
                namesCount = chart.ChartNamesTwo.Count;
            }

            for (int i = 0; i < namesCount; i++) {
                
            }

            return View(chart);
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
        [HttpPost]
        public ActionResult ChangeRow(Row row, string action) {
            if (action == "change") {
                row.Total = row.Quantity * row.Cost;
                db.Entry(row).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (action == "delete") {
                Row p = db.Rows.Find(row.RowId);
                db.Rows.Remove(p);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { year = row.Date.Year, month = row.Date.Month, day = row.Date.Day });
        }
        [HttpPost]
        public ActionResult BackInTime (DateTime currentDate, string action) {
            DateTime newDate = new DateTime();
            if (action == "back") {
                newDate = currentDate.AddDays(-1);
            }
            if (action == "forward") {
                newDate = currentDate.AddDays(1);
            }
            return RedirectToAction("Index", "Home", new { year = newDate.Year, month = newDate.Month, day = newDate.Day });
        }
        private List<decimal> FillTheChart (List<Row> rows, List<string> names) {
            List<decimal> totalList = new List<decimal>();
            foreach (var name in names) {
                decimal totalCount = 0;
                foreach (var row in rows) {
                    if (name == row.Name) {
                        totalCount += row.Total;
                    }
                }
                totalList.Add(totalCount);
            }
            return totalList;
        }
    }
}