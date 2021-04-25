using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppProject.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name ="نقش")]
        [Required(ErrorMessage = "یک نقش وارد کنید")]
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}