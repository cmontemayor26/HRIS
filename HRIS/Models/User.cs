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
    
    public partial class User
    {
        public int Userid { get; set; }
        public string Email { get; set; }
        public string Userlevel { get; set; }
        public string Password { get; set; }
        public Nullable<int> EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
    }
}
