using UniversitySystem.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models
{
    public class AdvisingRequest
    {
        [Key]
        public int RequestID {  get; set; }
        public int StudentID {  get; set; }
        public Student Student { get; set; }
        public int FacultyID {  get; set; }
        public Faculty Faculty { get; set; }
        public DateTime RequestDate { get; set; }

        public RequestStatus ReqStatus { get; set; }
        public string RequestText { get; set; }
    }
}
