//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRIS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkExperience
    {
        public int WorkExperienceID { get; set; }
        public Nullable<int> MasterlistID { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
    }
}
