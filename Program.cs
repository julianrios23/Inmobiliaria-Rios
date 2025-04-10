using Inmobiliaria_Rios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Asegúrate de haber instalado el paquete Pomelo.EntityFrameworkCore.MySql, por ejemplo:
// dotnet add package Pomelo.EntityFrameworkCore.MySql

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql("server=localhost;port=3306;database=inmobiliariarios;user=root;password=root", 
        ServerVersion.AutoDetect("server=localhost;port=3306;database=inmobiliariarios;user=root;password=root"))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddDbContext<Inmobiliaria_Rios.Data.ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

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

// Reemplazar UseEndpoints con MapControllerRoute
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Configurar Login como la vista inicial

app.Run();
