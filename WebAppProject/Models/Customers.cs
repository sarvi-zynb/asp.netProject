using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class Customers
    {
        [Key]
        public int CustomersId { get; set; }
        public int RoleId { get; set; }

        [Display(Name ="نام و نام خانوادگی")]
        [Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید.")]
        [MaxLength(60)]
        public string FullName { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "جنسیت خود را انتخاب کنید")]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Display(Name = "آدرس محل سکونت")]
        [Required(ErrorMessage = "آدرس محل سکونت خود را وارد کنید")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        public int PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [MaxLength(60)]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
        [MaxLength(30)]
        public string Password { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}