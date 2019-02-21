using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
    public class UserModel
    {
        public int Userid { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Userlevel { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Employee Number must be numeric")]
        public int EmployeeNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password length must be minimum of 8 characters")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password length must be minimum of 8 characters")]
        [CompareAttribute("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ProfilePic { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}