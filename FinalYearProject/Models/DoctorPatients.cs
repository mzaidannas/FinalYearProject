using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class DoctorPatients
    {
        int Id;
        string Name;

        public DoctorPatients(int Id , string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}