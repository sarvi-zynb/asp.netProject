using WebAppProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class WebAppProjectContext : DbContext
    {

        public WebAppProjectContext()
            : base("WebAppProjectContext")
        {

        }
        public DbSet<Category> AllCategory { get; set; }
        public DbSet<Customers> AllCustomers { get; set; }
        public DbSet<Emploee> AllEmploee { get; set; }
        public DbSet<Role> AllRole { get; set; }
        public DbSet<Orders> AllOrders { get; set; }
        public DbSet<OrderDetails> AllOrderDetails { get; set; }
        public DbSet<Products> AllProducts { get; set; }
    }
}