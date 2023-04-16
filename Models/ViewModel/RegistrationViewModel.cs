
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models.ViewModel
{
    public class RegistrationViewModel
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm not Matched")]
        [Display(Name ="Confirm Password")]
        public string Confirmpassword { get; set; }

    }
}
