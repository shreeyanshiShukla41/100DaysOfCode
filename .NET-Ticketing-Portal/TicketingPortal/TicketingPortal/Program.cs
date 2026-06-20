using Microsoft.EntityFrameworkCore;
using TicketingPortal.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
        options.SlidingExpiration = true;
        // YEH 3 LINES ADD KARNI HAIN LOCALHOST PAR COOKIE ALLOW KARNE KE LIYE:
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // HTTP aur HTTPS dono par chalega
        options.Cookie.SameSite = SameSiteMode.Lax; // Strict rules ko thoda relax karega
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
// ---- SYSTEM AUTOMATIC USER SEEDING START ----
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TicketingPortal.Data.ApplicationDbContext>();

        // Check karo agar Users table bilkul khali hai
        if (!context.Users.Any())
        {
            context.Users.Add(new TicketingPortal.Models.USER_MODEL
            {
                FULL_NAME = "Rahul Kumar",
                EMAIL = "rahul@acmecorp.in",
                PASSWORD = "bhai123", // Testing ke liye plain text password
                ROLE = "Admin"
            });
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Agar koi dikkat aaye toh background mein handle ho jaye
        Console.WriteLine("Data Seeding Error: " + ex.Message);
    }
}
// ---- SYSTEM AUTOMATIC USER SEEDING END ----


app.Run();
