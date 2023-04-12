namespace Student_Management_System.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Lect_Hours { get; set; }
        public int Lab_Hours { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        = new HashSet<Department>();


        

    }
}
