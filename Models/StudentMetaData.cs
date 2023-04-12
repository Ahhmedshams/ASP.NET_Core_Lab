using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Student_Management_System.Models
{
    [ModelMetadataType (typeof(StudentMetaData))]
    public partial class Student
    {
        // WE make a saprite metaData class in case if we use DataBase First

    }

    public class StudentMetaData
    {
       
        [Range(18, 39, ErrorMessage = "Age Must be between 18 and 39")]

        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password, ErrorMessage = "Confirm Password Must match with password")]
        [Display(Name = "Password Confiramation")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You Have to choose Department")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [ValidateNever]
        public virtual Department Department { get; set; }


    }
}
