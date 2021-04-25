using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class ProductViewModel
    {
        public int ProductsId { get; set; }

        public string ProductName { get; set; }

        public int UnitPrice { get; set; }
        public byte[] Image { get; set; }

        public int Quantity { get; set; }
    }
}