using Microsoft.AspNetCore.Mvc;

namespace TicketingPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
