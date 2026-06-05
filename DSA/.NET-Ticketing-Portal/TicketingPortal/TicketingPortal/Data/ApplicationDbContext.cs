using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketingPortal.Models;

namespace TicketingPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
