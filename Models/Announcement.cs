using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models

{
    public class Announcement
    {
        [Key]
        public int AnnouncementID { get; set; }
        public string Title {  get; set; }
        public string AnnouncementMessage { get; set; }
        public int CreatedBy {  get; set; }
        public Faculty Faculty { get; set; }
        public DateTime PostedDate { get; set; }
        public string TargetStudents {  get; set; }
    }
}
