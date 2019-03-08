using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{

    public class Essay
    {
        public string ApplicantID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay1 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay2 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay3 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay4 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay5 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay6 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay7 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay8 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay9 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay10 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay11 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay12 { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Essay13 { get; set; }
    }
}