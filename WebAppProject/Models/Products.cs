using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class Products
    {
        [Key]
        public int ProductsId { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "نام محصول را وارد کنید.")]
        [MaxLength(60)]
        public string ProductName { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "توضیحات را وارد کنید.")]
        [MaxLength(2000)]
        public string Description { get; set; }


        [Display(Name = "تعداد محصول در انبار")]
        [Required(ErrorMessage = "تعداد محصول در انبار را وارد کنید.")]
        public int UnitsInStock { get; set; }

        [Display(Name = "قیمت واحد")]
        [Required(ErrorMessage = "قیمت واحد را وارد کنید.")]
        public int UnitPrice { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "یک عکس انتخاب کنید.")]
        public byte[] Image { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}