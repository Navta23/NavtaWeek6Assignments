namespace BECSystem.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        //foreign key -> Course
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Foreign Key → User (Identity)
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ModeOfPayment { get; set; }
    }
}
