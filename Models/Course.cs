using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }
        public string CourseName { get; set; } 
        public string CourseDescription { get; set; } 
        public int CreditHours {  get; set; }
        public string Department { get; set; } 
        public string Semaster { get; set; } 
        public int? PrerequisiteCourseID {  get; set; }
        public int InstructorID {  get; set; }
        public string InstructorName { get; set;} 
        public int MaxCapcity {  get; set; }

    }
}
