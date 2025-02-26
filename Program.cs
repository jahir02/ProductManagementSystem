using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection
builder.Services.AddDbContext<ProductManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Con"));
});

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Redirect to Login page if not authenticated
        options.LogoutPath = "/Account/Logout"; // Logout URL
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session expiration time
        options.SlidingExpiration = true; // Renew session if active
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();
