using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Please enter your Email")]
        [Display(Name ="Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserLevel { get; set; }
    }
}