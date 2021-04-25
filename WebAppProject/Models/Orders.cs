using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class Orders
    {
        [Key]
        public int OrdersId { get; set; }
        public int CustomersId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReciveDate { get; set; }

        [ForeignKey("CustomersId")]
        public virtual Customers Customers { get; set; }

    }
}