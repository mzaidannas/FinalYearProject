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

    public partial class Patient_History
    {
        public int Pat_Id { get; set; }
        public string Confidential { get; set; }
        public string Public { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
