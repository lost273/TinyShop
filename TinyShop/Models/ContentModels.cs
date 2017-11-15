using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TinyShop.Models {
    public class Сonsumption {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
    public class Product{
        string Name { get; set; }
    }
    public class OneDayContext : DbContext {
        public OneDayContext () : base("DefaultConnection")  { }
        public DbSet<Сonsumption> Сonsumptions { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}