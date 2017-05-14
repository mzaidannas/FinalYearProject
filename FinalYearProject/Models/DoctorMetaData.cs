using System;
using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class DoctorMetaData
    {
        public int Person_Id { get; set; }
        public Nullable<double> Rating { get; set; }

        [DisplayAttribute(Name = "Career Start Date")]
        [DisplayFormat(NullDisplayText = "Date Not Specified")]//this text will save in DB if not specified in text field.
        [Required(ErrorMessage = "Career Start Date is required.")]

        public Nullable<System.DateTime> Career_Start { get; set; }

        [DisplayAttribute(Name = "Specialized Category")]
        [Required(ErrorMessage = "Category is required.")]
        public string Type { get; set; }

        [DisplayAttribute(Name = "Average Duration Per Patient")]
        [DisplayFormat(NullDisplayText = "Duration Not Specified")]//this text will save in DB if not specified in text field.
        [Required(ErrorMessage = "Average Duration is required.")]
        public Nullable<System.TimeSpan> Average_Duration { get; set; }

    }

    [MetadataType(typeof(DoctorMetaData))]
    public partial class Doctor
    {
    }
}