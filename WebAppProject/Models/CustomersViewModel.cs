using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class CustomersViewModel
    {
        public string CustomerFullName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReciveDate { get; set; }
        public string ProductName { get; set; }
        public byte[] Image { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}