using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models
   
{
    public class Admin
    {
        public int AdminID { get; set; }
        
        public string AdminName { get; set; } 
        
        public string Email {  get; set; }
        public string Role {  get; set; }
        public string Password { get; set; }

    }
}
