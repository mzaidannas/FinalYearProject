using System;

namespace FinalYearProject.Models
{
    public class MainPerson
    {
        //[Required(ErrorMessage = "Name can't be empty.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "CNIC can't be empty.")]
        public string CNIC { get; set; }

        //[Required(ErrorMessage = "Address can't be empty.")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Date of Birth can't be empty.")]
        public DateTime? DOB { get; set; }

        //[Required(ErrorMessage = "Gender can't be empty.")]
        public string Gender { get; set; }

        //[Required(ErrorMessage = "Career Start Date can't be empty.")]
        public DateTime Career_Start { get; set; }

        //[Required(ErrorMessage = "Insurance Number can't be empty.")]
        public string Insurance_No { get; set; }

        //[Required(ErrorMessage = "Doctor Category can't be empty.")]
        public string Type { get; set; }

        //[Required(ErrorMessage = "Blood Group can't be empty.")]
        public string Blood_Group { get; set; }

        //[Required(ErrorMessage = "Average Duration Time for each patient can't be empty.")]
        public int Average_Duration { get; set;}


        //[Required(ErrorMessage = "User Name can't be empty.")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Password can't be empty.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Email can't be empty.")]
        public string Email { get; set; }


        //[Required(ErrorMessage = "Phone Number can't be empty.")]
        public string Phone_no { get; set; }
    }
}