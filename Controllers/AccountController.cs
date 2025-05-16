using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Inmobiliaria_Rios.Data;
using Inmobiliaria_Rios.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Rios.Controllers;

public class AccountController : Controller
{
    private readonly Inmobiliaria_Rios.Data.ApplicationDbContext _context;

    public AccountController(Inmobiliaria_Rios.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Buscar usuario por mail y contraseña
        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.Mail == username && u.Contraseña == password); // Cambia 'Contraseña' por el nombre real de la columna

        if (usuario != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, (usuario.Nombre ?? "") + " " + (usuario.Apellido ?? "")),
                new Claim(ClaimTypes.Email, usuario.Mail ?? ""),
                new Claim(ClaimTypes.Role, usuario.Rol ?? "empleado"),
                new Claim("UsuarioId", usuario.Idusuarios.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Redirigir al index general (Home/Index)
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Usuario o contraseña incorrectos";
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Register(string username, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ModelState.AddModelError("PasswordMismatch", "Las contraseñas no coinciden.");
            return View();
        }

        // Aquí puedes agregar lógica para registrar al usuario
        TempData["Mensaje"] = "Registro exitoso. Ahora puedes iniciar sesión.";
        return RedirectToAction("Login", "Account");
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
