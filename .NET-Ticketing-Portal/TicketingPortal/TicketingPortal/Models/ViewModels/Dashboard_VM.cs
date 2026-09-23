using Microsoft.AspNetCore.Authentication;

namespace TicketingPortal.Models.ViewModels
{
    public class Dashboard_VM
    {
      public List<TICKET_MODEL> Tickets { get; set; } = new List<TICKET_MODEL>();
      //new List<TICKET_MODEL>(); ensures that the Tickets property is initialized to an empty list when a new instance of Dashboard_VM is created.

      int LeaveCount { get; set; } = 0;
        
    }
}
