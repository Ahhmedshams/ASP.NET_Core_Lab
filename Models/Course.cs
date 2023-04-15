using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_System.Models
{
    public  class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50, MinimumLength =1 ,ErrorMessage ="Coure name must be between 1 to 50 character.")]
        public string Name { get; set; }

        public string Description { get; set; }
        [Range(3,200, ErrorMessage = "Lecture Hours  must be between 3 to 200 character.")]
        [Display(Name="Lecture Hours")]
        public int Lect_Hours { get; set; }
        [Display(Name = "Labs Hours")]

        [Range(3, 100, ErrorMessage = "Lab Hours  must be between 3 to 100 character.")]
        public int Lab_Hours { get; set; }
        [ValidateNever]
        public virtual ICollection<Department> Departments { get; set; }
        = new HashSet<Department>();

        [ValidateNever]
        public virtual ICollection<StudentCourses> StudentCourses { get; set; }
        = new HashSet<StudentCourses>();

    }
}
