using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TinyShop.Models {
    public class Сonsumption {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Total { get {return this.Quantity * this.Cost; } }
    }
    public class OneDay {
        public DateTime Date { get; set; }
        public List<Сonsumption> Name { get; set; }
    }
    public class OneDayContext : DbContext {
        public OneDayContext () : base("DefaultConnection")  { }
        public DbSet<OneDay> OneDays { get; set; }
    }
}