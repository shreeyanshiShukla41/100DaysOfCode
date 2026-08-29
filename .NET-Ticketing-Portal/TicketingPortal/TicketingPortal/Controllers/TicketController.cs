using Microsoft.AspNetCore.Hosting; // Is namespace ki zaroorat padegi
using Microsoft.AspNetCore.Http;    // IFormFile ke liye
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;                    // Path aur File handling ke liye

using TicketingPortal.Data;    // Taaki ApplicationDbContext mil sake
using TicketingPortal.Models;
public class TicketController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment; // Naya environment variable

    // Constructor mein IWebHostEnvironment ko inject kiya
    public TicketController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    private void Dropdowns()
    {
        ViewBag.StatusList = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Value = "IN PROGRESS", Text = "IN PROGRESS" },
            new SelectListItem { Value = "COMPLETED", Text = "COMPLETED" },
            new SelectListItem { Value = "ON HOLD", Text = "ON HOLD" }
        }, "Value", "Text");
        ViewBag.PriorityList = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Value = "LOW", Text = "LOW" },
            new SelectListItem { Value = "MEDIUM", Text = "MEDIUM" },
            new SelectListItem { Value = "HIGH", Text = "HIGH" }
        }, "Value", "Text");

        ViewBag.CategoryList = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Value = "BUG", Text = "BUG" },
            new SelectListItem { Value = "FEATURE REQUEST", Text = "FEATURE REQUEST" },
            new SelectListItem { Value = "DOCUMENTATION", Text = "DOCUMENTATION" },
            new SelectListItem { Value = "SECURITY", Text = "SECURITY" },
            new SelectListItem { Value = "APPLICATION", Text = "APPLICATION" },
            new SelectListItem { Value = "DATABASE", Text = "DATABASE" },
            new SelectListItem { Value = "UI/UX", Text = "UI/UX" },
            new SelectListItem { Value = "TESTING", Text = "TESTING" },
            new SelectListItem { Value = "ETL/DATA INTEGRATION", Text = "ETL/DATA INTEGRATION" },
            new SelectListItem { Value = "OTHER", Text = "OTHER" }
        }, "Value", "Text");
        
    }


    public ActionResult CREATE()
    {
        Dropdowns(); // Dropdowns ko populate karne ke liye method call karo


        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CREATE(TICKET_MODEL ticket, IFormFile? attachmentFile) // <-- attachmentFile parameter pakda
    {
        // 1. Check karo kya user ne sach mein koi file select ki hai?
        if (attachmentFile != null && attachmentFile.Length > 0)
        {
            // 2. wwwroot folder ke andar 'uploads' naam ka rasta dhoondho
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

            // Agar aapke project mein 'uploads' folder nahi bana, toh yeh line khud bana degi
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 3. File ka unique naam banao taaki agar do users 'error.png' naam ki file dalen toh mix na ho
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(attachmentFile.FileName);

            // Physical computer par file save karne ka poora path
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 4. File ko folder mein physically copy karo (MERN ke fs.writeFile jaisa)
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await attachmentFile.CopyToAsync(fileStream);
            }

            // 5. Database ke liye relative path string set kar do
            ticket.AttachmentPath = "/uploads/" + uniqueFileName;         
        }

        if (ModelState.IsValid)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        Dropdowns(); // Agar model state invalid hai, toh dropdowns ko dobara populate karo
        return View(ticket);
    }
   

}