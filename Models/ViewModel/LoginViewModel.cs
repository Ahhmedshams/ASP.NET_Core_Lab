using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool IsPersisite { get; set; }= true;


      
    }
}
