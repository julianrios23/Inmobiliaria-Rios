using Inmobiliaria_Rios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql("server=localhost;port=3306;database=inmobiliariarios;user=root;password=root", 
        new MySqlServerVersion(new Version(8, 0, 33))) // Configuración para .NET 8.0.0
    .EnableSensitiveDataLogging() // Habilitar registro de datos sensibles
    .LogTo(Console.WriteLine, LogLevel.Information)); // Registrar consultas en la consola

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
