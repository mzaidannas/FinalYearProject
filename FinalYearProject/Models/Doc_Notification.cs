using System;

namespace FinalYearProject.Models
{
    public class Doc_Notification
    {
        public int Per_Id { get; set; }
        public int Doc_Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Status { get; set; }

    }
}