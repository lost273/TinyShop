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
            ChartInfo chartMonth = new ChartInfo();

            //get the rows according the input date period
            List<Row> rowsOne = db.Rows.Where(row => (row.Date.Month == dateRequestOne.Month) && (row.Date.Year == dateRequestOne.Year)).ToList();
            List<Row> rowsTwo = db.Rows.Where(row => (row.Date.Month == dateRequestTwo.Month) && (row.Date.Year == dateRequestTwo.Year)).ToList();

            //get data for the whole year
            List<string> monthsYearOne = db.Rows.Where(row => row.Date.Year == dateRequestOne.Year).Select(m => m.Date.Month.ToString()).Distinct().ToList();
            List<string> monthsYearTwo = new List<string>();
            List<decimal> totalYearOne = new List<decimal>();
            List<decimal> totalYearTwo = new List<decimal>();
            if (dateRequestOne.Year != dateRequestTwo.Year) {
                monthsYearTwo = db.Rows.Where(row => row.Date.Year == dateRequestTwo.Year).Select(m => m.Date.Month.ToString()).Distinct().ToList();
                foreach (string month in monthsYearTwo) {
                    totalYearTwo.Add(db.Rows.Where(m => (m.Date.Month.ToString() == month) && (m.Date.Year == dateRequestTwo.Year)).Select(t => t.Total).Sum());
                }
            }
            foreach (string month in monthsYearOne) {
                totalYearOne.Add(db.Rows.Where(m => (m.Date.Month.ToString() == month) && (m.Date.Year == dateRequestOne.Year)).Select(t => t.Total).Sum());
            }

            List<string> ChartNamesOne = rowsOne.Select(n => n.Name).Distinct().ToList();
            List<string> ChartNamesTwo = rowsTwo.Select(n => n.Name).Distinct().ToList();
            List<decimal> ChartTotalOne = FillTheChart(rowsOne, ChartNamesOne);
            List<decimal> ChartTotalTwo = FillTheChart(rowsTwo, ChartNamesTwo);

            chartMonth = DataMerge(ChartNamesOne, ChartNamesTwo, ChartTotalOne, ChartTotalTwo);
            ViewBag.chartYear = DataMerge(monthsYearOne, monthsYearTwo, totalYearOne, totalYearTwo);
            //available months and years
            ViewBag.requestYears = db.Rows.Select(r => r.Date.Year).Distinct().ToList();
            ViewBag.requestMonths = db.Rows.Select(r => r.Date.Month).Distinct().ToList();

            //title for the chart
            ViewBag.dateRequest = $"{dateRequestOne.Month}.{dateRequestOne.Year} - {dateRequestTwo.Month}.{dateRequestTwo.Year}";
            ViewBag.periodOne = $"{dateRequestOne.Year}";
            ViewBag.periodTwo = $"{dateRequestTwo.Year}";
            return View(chartMonth);
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
        //merge the strings and compare numbers for chart whom contain two datasets
        private ChartInfo DataMerge (List<string> oneString, List<string> twoString, List<decimal> oneNumber, List<decimal> twoNumber) {
            List<string> commonString = new List<string>();
            List<decimal> newNumberOne = new List<decimal>();
            List<decimal> newNumberTwo = new List<decimal>();
            ChartInfo chart = new ChartInfo();

            //merge the strings and delete repeated values
            commonString = oneString.Concat(twoString).Distinct().ToList();
            //sort numbers according the strings
            foreach (string strOne in commonString) {
                int i = 0;
                bool strExist = false;
                foreach (string strTwo in oneString) {
                    if (strOne == strTwo) {
                        newNumberOne.Add(oneNumber[i]);
                        strExist = true;
                        break;
                    }
                    i++;
                }
                if (!strExist) {
                    newNumberOne.Add(0);
                }
                i = 0;
                strExist = false;
                foreach (string strTwo in twoString) {
                    if (strOne == strTwo) {
                        newNumberTwo.Add(twoNumber[i]);
                        strExist = true;
                        break;
                    }
                    i++;
                }
                if (!strExist) {
                    newNumberTwo.Add(0);
                }
            }
            chart.ChartTotalOne = newNumberOne;
            chart.ChartNamesOne = commonString;
            chart.ChartTotalTwo = newNumberTwo;
            return chart;            
        }
    }
}