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
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public decimal Total { get; set; }
    }

    public class Product{
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class ChartInfo {
        public List<decimal> ChartTotalOne { get; set; }
        public List<string> ChartNamesOne { get; set; }
        public List<decimal> ChartTotalTwo { get; set; }
        public List<string> ChartNamesTwo { get; set; }
    }
    public class UserTimeZone {
        [Key]
        public int Id { get; set; }
        public string Zone { get; set; }
    }
    public class OneDayContext : DbContext {
        public OneDayContext () : base("DefaultConnection")  { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<UserTimeZone> UserTimeZones { get; set; }
    }
}