using UniversitySystem.Enums;

namespace UniversitySystem.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; } = new Student();
        public float Amount {  get; set; }
        public DateTime Date { get; set; }
        public paymentMethod Status { get; set; }

    }
}
