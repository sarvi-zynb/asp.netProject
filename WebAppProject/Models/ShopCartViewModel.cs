using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class ShopCartViewModel
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
    }
}