namespace Student_Management_System.Models
{
    public  class CourseComparer : IEqualityComparer<Course>
    {
        // Course are equal if their names are equal.
        public bool Equals(Course x, Course y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return  x.Name == y.Name;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Course course)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(course, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = course.Name == null ? 0 : course.Name.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = course.Id.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }
}
