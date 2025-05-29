using UniversitySystem.Enums;

namespace UniversitySystem.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID {  get; set; }
        public Student Student { get; set; }
        public int CourseID {  get; set; }
        public string Semester { get; set; }
        public string Grade {  get; set; }
        public double AttendacePercentage {  get; set; }
        public EnrollmentStatus EnrollStatus {  get; set; }

    }
}
