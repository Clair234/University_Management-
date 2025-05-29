using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystem.Models
{
    public class Faculty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacultyID { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string phone {  get; set; }
        public string Department {  get; set; }
        public string Title { get; set; }

        public string Password { get; set; }


        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
