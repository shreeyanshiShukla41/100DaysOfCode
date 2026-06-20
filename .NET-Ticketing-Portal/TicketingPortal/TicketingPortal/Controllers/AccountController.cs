using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TicketingPortal.Data;
using TicketingPortal.Models;

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
        public async Task<IActionResult> Login(string email, string password, string rememberMe)
        {
            bool isPersistent = (!string.IsNullOrEmpty(rememberMe) && rememberMe.ToLower() == "true");

            var user = _context.Users.FirstOrDefault(u => u.EMAIL == email && u.PASSWORD == password);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid Email or Password!";
                return View();
            }

            // --- CREDENTIALS REMEMBER LOGIC (EMAIL COOKIE) ---
            if (isPersistent)
            {
                // Agar remember me checked hai, toh email ko ek separate cookie mein 14 din ke liye save kar lo
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(14),
                    HttpOnly = false // False rakhenge taaki agar HTML/JS ko read karna ho toh dikh sake
                };
                Response.Cookies.Append("RememberedEmail", email, cookieOptions);
            }
            else
            {
                // Agar unchecked hai, toh agar purani koi email cookie padi hai toh use delete kar do
                Response.Cookies.Delete("RememberedEmail");
            }

            // Baki aapka authentication code bilkul same rahega...
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.FULL_NAME), new Claim(ClaimTypes.Email, user.EMAIL) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = isPersistent, ExpiresUtc = isPersistent ? DateTimeOffset.UtcNow.AddDays(14) : null };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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

        // 1. GET: Register Page dikhane ke liye
        [HttpGet]
        public IActionResult Register() => View();

        // 2. POST: Register Form ka data database mein save karne ke liye
        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string email, string password)
        {
            // Check karo ki kahin ye email pehle se toh register nahi hai
            var existingUser = _context.Users.FirstOrDefault(u => u.EMAIL == email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Email is already registered!";
                return View();
            }

            // Naya user object banao
            var newUser = new USER_MODEL
            {
                FULL_NAME = fullName,
                EMAIL = email,
                PASSWORD = password, // Real app mein isey hash karna chahiye, abhi testing ke liye plain text hai
                ROLE = "User" // Default role
            };

            // Database mein add aur save karo (MERN mein User.create jaisa)
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Register hote hi seedhe Login page par bhej do
            return RedirectToAction("Login");
        }
    }
}