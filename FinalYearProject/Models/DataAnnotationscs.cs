using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.Models
{
    public class LoginMetadata
    {

        [Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }

    [MetadataType(typeof(LoginMetadata))]
    public partial class Login
    {
    }
}