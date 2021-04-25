using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "یک نام برای دسته بند ی ها انتخاب کنید.")]
        [MaxLength(60)]
        public string CategoryName { get; set; }
    }
}