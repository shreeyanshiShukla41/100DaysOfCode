using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TicketingPortal.Data;

namespace TicketingPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Ekdum dhyan se check karo: u.EMAIL == email aur u.PASSWORD == password hona chahiye
            var user = _context.Users.FirstOrDefault(u => u.EMAIL == email && u.PASSWORD == password);

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FULL_NAME),
            new Claim(ClaimTypes.Email, user.EMAIL),
            new Claim(ClaimTypes.Role, user.ROLE ?? "User")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid Email or Password!";
            return View();
        }
    }
}