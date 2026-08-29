using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TicketingPortal.Data;
using TicketingPortal.Models;

namespace TicketingPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var latestAnnouncement = await _context.Announcements
        .Where(a => a.IsActive)
        .OrderByDescending(a => a.CreatedDate)
        .FirstOrDefaultAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
