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
    public class OneDayContext : DbContext {
        public OneDayContext () : base("DefaultConnection")  { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Row> Rows { get; set; }
    }
}