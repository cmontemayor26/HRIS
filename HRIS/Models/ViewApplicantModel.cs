using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace HRIS.Models
{
    public class ViewApplicantModel
    {
        public int MasterlistID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Birthday { get; set; }
        public string MaritalStatus { get; set; }
        public string JobTitle { get; set; }
        public string Street_Address1 { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Street_Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string ContactNumber { get; set; }
        public string PersonalEmail { get; set; }

        public string CompanyEmail { get; set; }
        public string CompanyName { get; set; }
        public string WorkExperienceJobTitle { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
    }
}