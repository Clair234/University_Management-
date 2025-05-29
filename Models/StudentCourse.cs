namespace UniversitySystem.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
