using System.ComponentModel.DataAnnotations;
using UniversitySystem.Enums;

namespace UniversitySystem.Models
{
    public class Attendance
    {
        [Key]public int EnrllmentID { get; set; }
        [Required]
        public DateTime Date {  get; set; }
        public AttendanceStatus AttendStatus { get; set; }
    }
}
