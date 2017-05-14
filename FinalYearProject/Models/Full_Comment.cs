using System;

namespace FinalYearProject.Models
{
    public class Full_Comment
    {
        public int Comment_Id { get; set; }
        public int Doc_Id { get; set; }
        public string Pat_Name { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
    }
}