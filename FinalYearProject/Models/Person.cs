//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalYearProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        public int Person_Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Image_Path { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Login Login { get; set; }
        public virtual Patient Patient { get; set; }
    }
}