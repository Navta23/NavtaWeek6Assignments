namespace BECSystem.Models
{
    public class Enquiry
    {
        public int EnquiryID { get; set; }
        public DateTime EnquiryDate { get; set; }
        //foriegn key
        public string UserId { get; set; }
        //navigation property
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
  
        public string EnquiryType { get; set; }
        public string Status { get; set; } = "Pending";

    }
}
