using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_System.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 2, ErrorMessage = "Your Name should be at least Two character and not more than fifteen ")]
        public string Name { get; set; }
        [Range(15, 50, ErrorMessage = "Capacty Shoud Be between 15:50")]
        public int Capacity { get; set; }
        [ValidateNever]
        public ICollection<Student> Students { get; set;}
        [ValidateNever]
        public ICollection<Course> courses { get; set;} = new HashSet<Course>();



       
    }
}
