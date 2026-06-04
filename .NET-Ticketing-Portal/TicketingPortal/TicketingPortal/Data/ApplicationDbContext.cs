using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketingPortal.Models;

namespace TicketingPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor that passes our connection configuration to the base DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // This DbSet creates a "Tickets" table in SQL Server based on our C# Ticket model
        public DbSet<TICKET_MODEL> Tickets { get; set; }
    }
}