using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{

    public class Full_Doctor
    {
        public int Id { get; set; }
        public DateTime Career_Start { get; set; }
        public double Rating { get; set; }
        public string Type { get; set; }
        public TimeSpan Average_Duration { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Image_Path { get; set; }
        public List<Full_Comment> Comments { get; set; }

    }
}