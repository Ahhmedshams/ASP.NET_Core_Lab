

using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Models.ViewModel
{
    public class RoleViewModel
    {
        [Key]
        [Required]
        public string RoleName { get; set; }
    }
}
