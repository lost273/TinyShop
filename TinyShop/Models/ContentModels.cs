using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TinyShop.Models {

    public class Row {
        [Key]
        public int RowId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Total { get; set; }
    }

    public class Product{
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
    public class ChartInfo {
        public List<decimal> ChartTotalOne { get; set; }
        public List<string> ChartNamesOne { get; set; }
        public List<decimal> ChartTotalTwo { get; set; }
        public List<string> ChartNamesTwo { get; set; }
        public List<int> Years { get; set; }
        public List<int> Months { get; set; }
        public List<int> MonthsNames { get; set; }
        public List<decimal> TotalYearOne { get; set; }
        public List<decimal> TotalYearTwo { get; set; }
    }
    public class OneDayContext : DbContext {
        public OneDayContext () : base("DefaultConnection")  { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Row> Rows { get; set; }
    }
}