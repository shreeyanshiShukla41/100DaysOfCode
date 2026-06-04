namespace TicketingPortal.Models
{
    public class TICKET_MODEL
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "In Progress";

        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly UpdatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public string AssignedTo { get; set; } = string.Empty;

        public string Priority { get; set; } = "Medium";

        public string ProjectName { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int EstimatedHours { get; set; } = 0;

    }
}
