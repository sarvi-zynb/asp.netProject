using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrdersId { get; set; }
        public int ProductId { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }

        [ForeignKey("OrdersId")]
        public virtual Orders Orders { get; set; }
    }
}