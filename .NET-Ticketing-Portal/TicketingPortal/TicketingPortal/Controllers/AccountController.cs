using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public async Task<IActionResult> Login(string email, string password)
        {
            // 1. Database mein user dhoondho
            var user = _context.Users.FirstOrDefault(u => u.EMAIL == email && u.PASSWORD == password);

            // 2. Agar user na mile, toh wapas View par bhejo error message ke sath
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid Email or Password!";
                return View();
            }

            // 3. Agar user mil jaye, toh uski ID (Claims) banao
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FULL_NAME),
                new Claim(ClaimTypes.Email, user.EMAIL),
                new Claim(ClaimTypes.Role, user.ROLE ?? "User") // Role wapas add kar diya
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 4. Browser mein Cookie drop karo
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            // 5. Login ke baad seedhe Dashboard (Home/Index) par bhej do
            return RedirectToAction("Index", "Home");
        }

        // --- NAYA FEATURE: LOGOUT ---
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Browser se cookie delete kar do
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Wapas login page par bhej do
            return RedirectToAction("Login", "Account");
        }
    }
}