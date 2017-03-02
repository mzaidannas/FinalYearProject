using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class Full_Perscription
    {
        public int Id { get; set; }
        public string Doctor_Name { get; set; }
        public string Patient_Name { get; set; }
        public string Prescription_Name { get; set; }
        public string Type { get; set; }
        public int? Frequency { get; set; }
        public int? Amount { get; set; }
    }
}