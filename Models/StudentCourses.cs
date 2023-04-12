using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_System.Models
{
    public class StudentCourses
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public int? Degree { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set;}
    }
}
