using GoTrip.Data;
using GoTrip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric= true;
    options.Password.RequireDigit= true;
    options.Password.RequireLowercase= true;
    options.Password.RequireUppercase= true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(20);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<TripDbContext>();
builder.Services.AddDbContext<TripDbContext>(options =>
{
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GoTrip;Trusted_Connection=True;TrustServerCertificate=True;");
});

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );


app.Run();
