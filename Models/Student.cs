using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Student
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public string NationalID {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender {  get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public double GPA {  get; set; }
        public int TotalCreditHours { get; set; }
        public string EnrollmentStatus {  get; set; }
        public int AdvisorID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

    }
}
