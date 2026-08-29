namespace TicketingPortal.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string PostedBy { get; set; } = "HR";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
